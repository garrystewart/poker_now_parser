namespace PNP.Models
{
    public class Hand
    {
        public Hand(ushort number)
        {
            Number = number;
        }

        public ushort Number { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public TimeSpan Duration => EndedAt - StartedAt;

        public Player Dealer { get; set; }
        public Player SmallBlind { get; set; }
        public Player BigBlind { get; set; }

        public int SmallBlindAmount { get; set; }
        public int BigBlindAmount { get; set; }

        public IEnumerable<PlayerHand> PlayerHands { get; set; }
        public IEnumerable<Player> Players { get; set; }

        public class Flop
        {
            public string FlopCards { get; set; }
            public int StartingPot { get; set; }
            public int EndPot { get; set; }
            public DateTime StartedAt { get; set; }
            public DateTime EndedAt { get; set; }
            public IEnumerable<Player> Players { get; set; }
        }
        
        public class Turn
        {
            public string TurnCard { get; set; }
            public int StartingPot { get; set; }
            public int EndPot { get; set; }
            public DateTime StartedAt { get; set; }
            public DateTime EndedAt { get; set; }
            public IEnumerable<Player> Players { get; set; }
        }

        public class River
        {
            public string RiverCard { get; set; }
            public int StartingPot { get; set; }
            public int EndPot { get; set; }
            public DateTime StartedAt { get; set; }
            public DateTime EndedAt { get; set; }
            public IEnumerable<Player> Players { get; set; }
        }

        public class PlayerHand
        {
            public Player Player { get; set; }
            public string Hand { get; set; }
        }
    }
}
