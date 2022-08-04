using System.ComponentModel.DataAnnotations;

namespace PNP.ViewModels
{
    public class HomeVM
    {
        [Display(Name = "Poker Now ID")]
        public string? Id { get; set; }
    }
}