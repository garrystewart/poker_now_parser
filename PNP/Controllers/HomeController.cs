using Microsoft.AspNetCore.Mvc;
using PNP.Models;
using PNP.Services;
using PNP.ViewModels;
using System.Diagnostics;

namespace PNP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly MessageService _messageService;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, MessageService messageService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeVM());
        }

        [HttpPost]
        public IActionResult Index(string id)
        {
            try
            {
                JSON? jsonResponse = _httpClient.GetFromJsonAsync<JSON>($"https://www.pokernow.club/games/{id}/log").Result;

                foreach (JSON.Log log in jsonResponse.Logs)
                {
                    Debug.WriteLine(_messageService.GetMessageType(log.Message).ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Statistics(string id)
        {
            StatisticsVM model = new();

            try
            {
                JSON? jsonResponse = _httpClient.GetFromJsonAsync<JSON>($"https://www.pokernow.club/games/{id}/log").Result;

                Game game = new(_messageService, jsonResponse);

                model.ChipsInPlay = game.ChipsInPlay;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}