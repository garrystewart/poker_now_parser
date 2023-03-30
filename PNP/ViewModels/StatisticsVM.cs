using PNP.Models;
using PNP.Services;
using System.ComponentModel.DataAnnotations;
using static PNP.Services.ConnectionHostedService;

namespace PNP.ViewModels
{
    public class StatisticsVM
    {
        public StatisticsVM(Game game, Response response)
        {
            ChipsInPlay = game.ChipsInPlay;
            HandsPlayed = game.HandsPlayed;
            SmallBlinds = game.SmallBlinds;
            BigBlinds = game.BigBlinds;
            UncalledBets = game.UncalledBets;

            Flops = game.Flops;
            Turns = game.Turns;
            Rivers = game.Rivers;

            Bets = game.Bets;
            Raises = game.Raises;
            Checks = game.Checks;
            AllIns = game.AllIns;
            Shows = game.Shows;
            Calls = game.Calls;

            LastUpdated = response.LastUpdated;

            Hands = game.Hands;
        }

        public DateTime LastUpdated { get; set; }

        public int ChipsInPlay { get; set; }
        public ushort HandsPlayed { get; set; }
        public ushort SmallBlinds { get; set; }
        public ushort BigBlinds { get; set; }
        public ushort UncalledBets { get; set; }

        public ushort Flops { get; set; }
        public ushort Turns { get; set; }
        public ushort Rivers { get; set; }

        public ushort Calls { get; set; }
        public ushort Raises { get; set; }
        public ushort Bets { get; set; }
        public ushort Checks { get; set; }
        public ushort AllIns { get; set; }
        public ushort Shows { get; set; }

        public IEnumerable<Hand> Hands { get; set; }
    }
}