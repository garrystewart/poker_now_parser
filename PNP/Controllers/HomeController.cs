using Microsoft.AspNetCore.Mvc;
using PNP.Models;
using PNP.Services;
using PNP.ViewModels;
using System.Diagnostics;
using static PNP.Services.ConnectionHostedService;

namespace PNP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly ConnectionHostedService _connectionHostedService;

        public HomeController(
            ILogger<HomeController> logger, 
            HttpClient httpClient,
            ConnectionHostedService connectionHostedService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _connectionHostedService = connectionHostedService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeVM());
        }

        [HttpPost]
        public IActionResult Index(string id)
        {
            // add the id to the hosted service
            if (!string.IsNullOrWhiteSpace(id))
            {
                _connectionHostedService.AddId(id);
            }            

            return View();
        }

        [HttpGet]
        public IActionResult Statistics(string id)
        {
            try
            {
                Response? response = _connectionHostedService.GetResponse(id);

                if (response is not null)
                {
                    Game game = new(response.Json);

                    return View(new StatisticsVM(game, response));
                }
                else
                {
                    throw new Exception("response is null");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private JSON? GetLog(string id)
        {
            return _httpClient.GetFromJsonAsync<JSON>($"https://www.pokernow.club/games/{id}/log").Result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}