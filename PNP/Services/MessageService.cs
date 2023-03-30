using PNP.Enums;
using PNP.Models;
using System.Text.RegularExpressions;

namespace PNP.Services
{
    public static class MessageService
    {
        private static IDictionary<Regex, MessageType> _regexEnumDictionary = new Dictionary<Regex, MessageType>()
        {
            // admin functions
            { new Regex("^The player \".* @ .*\" quits the game with a stack of [0-9]+.$"), MessageType.ThePlayerQuitsTheGameWithAStack },
            { new Regex("^The player \".* @ .*\" joined the game with a stack of [0-9]+.$"), MessageType.ThePlayerJoinedTheGameWithAStackOf },
            { new Regex("^The admin approved the player \".* @ .*\" participation with a stack of [0-9]+.$"), MessageType.TheAdminApprovedThePlayerParticipationWithAStackOf },
            { new Regex("^The player \".* @ .*\" requested a seat.$"), MessageType.ThePlayerRequestedASeat },
            { new Regex("^The player \".* @ .*\" stand up with the stack of [0-9]+.$"), MessageType.ThePlayerStandUpWithTheStackOf },
            { new Regex("^The player \".* @ .*\" sit back with the stack of [0-9]+.$"), MessageType.ThePlayerSitBackWithTheStackOf },

            // game actions
            { new Regex("^-- starting hand #[0-9]+  \\(No Limit Texas Hold'em\\) \\(dealer: \".* @ .*\"\\) --$"), MessageType.StartingHand },
            { new Regex("^-- ending hand #[0-9]+ --$"), MessageType.EndingHand },
            { new Regex("^\".* @ .*\" posts a small blind of [0-9]+$"), MessageType.PostsASmallBlindOf },
            { new Regex("^\".* @ .*\" posts a big blind of [0-9]+$"), MessageType.PostsABigBlindOf },

            // board
            { new Regex("^Flop:  \\[.*\\]$"), MessageType.Flop },
            { new Regex("^Turn: .* \\[.*\\]$"), MessageType.Turn },
            { new Regex("^River: .* \\[.*\\]$"), MessageType.River },

            // player actions        
            { new Regex("^\".* @ .*\" bets [0-9]+$"), MessageType.Bets },
            { new Regex("^\".* @ .*\" calls [0-9]+$"), MessageType.Calls },
            { new Regex("^\".* @ .*\" calls 2 and go all in$"), MessageType.CallsAndGoAllIn },
            { new Regex("^\".* @ .*\" checks$"), MessageType.Checks },
            { new Regex("^\".* @ .*\" raises to [0-9]+$"), MessageType.RaisesTo },

            // results
            { new Regex("^Player stacks: #[0-9]+ \".* @ .*\" \\([0-9]+\\).*$"), MessageType.PlayerStacks },
            { new Regex("^\".* @ .*\" collected [0-9]+ from pot with .* \\(combination: .*\\)$"), MessageType.CollectedFromPotWith },
            { new Regex("^\".* @ .*\" shows a .*.$"), MessageType.ShowsA },
            { new Regex("^Uncalled bet of [0-9]+ returned to \".* @ .*\"$"), MessageType.UncalledBetOfReturnedTo },
            { new Regex("^Your hand is .*$"), MessageType.YourHandIs }
        };

        public static MessageType GetMessageType(string message)
        {
            foreach (var keyValuePair in _regexEnumDictionary)
            {
                if (keyValuePair.Key.IsMatch(message))
                {
                    return keyValuePair.Value;
                }
            }

            throw new NotImplementedException($"{message} is not implemented");
        }

        public static IEnumerable<JSON.Log> GetLogsByMessageType(MessageType messageType, JSON json)
        {
            return json.Logs.Where(l => GetRegex(messageType).IsMatch(l.Message)).ToList();
        }

        public static Regex GetRegex(MessageType messageType)
        {
            return _regexEnumDictionary.Single(r => r.Value == messageType).Key;
        }

        public enum MessageType
        {
            ThePlayerQuitsTheGameWithAStack,
            ThePlayerJoinedTheGameWithAStackOf,
            TheAdminApprovedThePlayerParticipationWithAStackOf,
            ThePlayerRequestedASeat,
            ThePlayerStandUpWithTheStackOf,
            ThePlayerSitBackWithTheStackOf,

            // game actions
            StartingHand,
            EndingHand,
            PostsASmallBlindOf,
            PostsABigBlindOf,

            // board        
            Flop,
            Turn,
            River,

            // player actions        
            Bets,
            Calls,
            CallsAndGoAllIn,
            Checks,
            RaisesTo,

            // results
            PlayerStacks,
            CollectedFromPotWith,
            ShowsA,
            UncalledBetOfReturnedTo,
            YourHandIs,
        }

        public static void ScanForMissingMessages(JSON json)
        {
            foreach (JSON.Log log in json.Logs)
            {
                bool found = false;

                foreach (var keyValuePair in _regexEnumDictionary)
                {
                    if (keyValuePair.Key.IsMatch(log.Message))
                    {
                        found = true;
                        continue;
                    }

                    if (!found)
                    {
                        throw new NotImplementedException($"No MessageType for message: {log.Message}");
                    }
                }
            }
        }
    }
}
