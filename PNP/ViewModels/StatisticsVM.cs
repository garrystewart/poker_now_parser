using System.ComponentModel.DataAnnotations;

namespace PNP.ViewModels
{
    public class StatisticsVM
    {
        public int ChipsInPlay { get; set; }
        public short HandsPlayed { get; set; }
        public short SmallBlinds { get; set; }
        public short BigBlinds { get; set; }
        public short UncalledBets { get; set; }

        public short Flops { get; set; }
        public short Turns { get; set; }
        public short Rivers { get; set; }

        public short Calls { get; set; }
        public short Bets { get; set; }
        public short Checks { get; set; }
        public short AllIns { get; set; }
        public short Shows { get; set; }
    }
}