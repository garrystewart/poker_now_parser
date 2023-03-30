using PNP.Enums;
using PNP.Services;
using System.Text.RegularExpressions;
using static PNP.Services.MessageService;

namespace PNP.Models
{
    public class Game
    {
        public Game(JSON json)
        {
            ChipsInPlay = GetChipsInPlay(json);
            HandsPlayed = GetHandsPlayed(json);
            SmallBlinds = GetSmallBlinds(json);
            BigBlinds = GetBigBlinds(json);
            UncalledBets = GetUncalledBets(json);

            Flops = GetFlops(json);
            Turns = GetTurns(json);
            Rivers = GetRivers(json);

            Calls = GetCalls(json);
            Raises = GetRaisesTo(json);
            Bets = GetBets(json);
            Checks = GetChecks(json);
            AllIns = GetCallsAndGoAllIns(json);
            Shows = GetShows(json);

            Hands = HandService.GetHands(json, HandsPlayed);
        }

        public string Id { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }

        public IEnumerable<Hand> Hands { get; set; }

        public IEnumerable<Player> AllPlayers { get; set; } = Enumerable.Empty<Player>();
        public IEnumerable<Player> PlayersRequestingASeat { get; set; } = Enumerable.Empty<Player>();
        public IEnumerable<Player> PlayersApproved { get; set; } = Enumerable.Empty<Player>();
        public IEnumerable<Player> PlayersStoodUp { get; set; } = Enumerable.Empty<Player>();
        public IEnumerable<Player> PlayersSatDown { get; set; } = Enumerable.Empty<Player>();


        public Player? Dealer { get; set; }
        public Player? SmallBlind { get; set; }
        public Player? BigBlind { get; set; }


        public int ChipsInPlay { get; set; }
        public ushort HandsPlayed { get; set; }
        public ushort SmallBlinds { get; set; }
        public ushort BigBlinds { get; set; }
        public ushort UncalledBets { get; set; }


        public ushort Flops { get; set; }
        public ushort Turns { get; set; }
        public ushort Rivers { get; set; }


        public ushort Raises { get; set; }
        public ushort Calls { get; set; }
        public ushort Bets { get; set; }
        public ushort Checks { get; set; }
        public ushort AllIns { get; set; }
        public ushort Shows { get; set; }

        private int GetChipsInPlay(JSON json)
        {
            // Player stacks: #4 "Garry Stewart @ 89BxM1Wz6Q" (122) | #6 "Garry Stewart 2 @ 7dHPQQOOVK" (123)
            // We're after the total of everything in brackets

            JSON.Log latestPlayerStacksLog = json.Logs.First(l => GetRegex(MessageType.PlayerStacks).IsMatch(l.Message));

            int totalStack = 0;

            Regex regex = new Regex("\\([0-9]+\\)"); // (122)

            MatchCollection matches = regex.Matches(latestPlayerStacksLog.Message);

            foreach (Match match in matches)
            {
                totalStack += Int32.Parse(match.Value.Substring(1, match.Value.Length - 2));
            }

            return totalStack;
        }

        private ushort GetHandsPlayed(JSON json)
        {
            // -- ending hand #2 --
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.EndingHand).IsMatch(l.Message));
        }

        private ushort GetSmallBlinds(JSON json)
        {
            // "Garry Stewart @ 89BxM1Wz6Q" posts a small blind of 10
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.PostsASmallBlindOf).IsMatch(l.Message));
        }

        private ushort GetBigBlinds(JSON json)
        {
            // "Garry Stewart @ 89BxM1Wz6Q" posts a big blind of 20
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.PostsABigBlindOf).IsMatch(l.Message));
        }

        private ushort GetUncalledBets(JSON json)
        {
            // Uncalled bet of 18 returned to "Garry Stewart 2 @ 7dHPQQOOVK"
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.UncalledBetOfReturnedTo).IsMatch(l.Message));
        }

        private ushort GetFlops(JSON json)
        {
            // Flop:  [9♦, 3♥, 10♠]
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.Flop).IsMatch(l.Message));
        }

        private ushort GetTurns(JSON json)
        {
            // Turn: 9♦, 3♥, 10♠ [A♥]
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.Turn).IsMatch(l.Message));
        }

        private ushort GetRivers(JSON json)
        {
            // River: 9♦, 3♥, 10♠, A♥ [K♣]
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.River).IsMatch(l.Message));
        }

        private ushort GetRaisesTo(JSON json)
        {
            // "Garry Stewart @ 7dHPQQOOVK" raises to 40
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.RaisesTo).IsMatch(l.Message));
        }

        private ushort GetCalls(JSON json)
        {
            // "Garry Stewart 2 @ 7dHPQQOOVK" calls 20
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.Calls).IsMatch(l.Message));
        }

        private ushort GetChecks(JSON json)
        {
            // "Garry Stewart @ 89BxM1Wz6Q" checks
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.Checks).IsMatch(l.Message));
        }

        private ushort GetBets(JSON json)
        {
            // "Garry Stewart @ 89BxM1Wz6Q" bets 20"
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.Bets).IsMatch(l.Message));
        }

        private ushort GetCallsAndGoAllIns(JSON json)
        {
            // "Garry Stewart @ 89BxM1Wz6Q" calls 2 and go all in
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.CallsAndGoAllIn).IsMatch(l.Message));
        }

        private ushort GetShows(JSON json)
        {
            // "Garry Stewart @ 89BxM1Wz6Q" shows a 2♠, 7♥.
            return (ushort)json.Logs.Count(l => GetRegex(MessageType.ShowsA).IsMatch(l.Message));
        }
    }
}