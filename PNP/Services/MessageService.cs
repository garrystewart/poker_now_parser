using PNP.Enums;
using System.Text.RegularExpressions;

namespace PNP.Services
{
    public class MessageService
    {
        IDictionary<Regex, Message.Type> _regexEnumDictionary = new Dictionary<Regex, Message.Type>()
        {
            // admin functions
            { new Regex("^The player \".* @ .*\" quits the game with a stack of [0-9]+.$"), Message.Type.ThePlayerQuitsTheGameWithAStack },
            { new Regex("^The player \".* @ .*\" joined the game with a stack of [0-9]+.$"), Message.Type.ThePlayerJoinedTheGameWithAStackOf },
            { new Regex("^The admin approved the player \".* @ .*\" participation with a stack of [0-9]+.$"), Message.Type.TheAdminApprovedThePlayerParticipationWithAStackOf },
            { new Regex("^The player \".* @ .*\" requested a seat.$"), Message.Type.ThePlayerRequestedASeat },
            { new Regex("^The player \".* @ .*\" stand up with the stack of [0-9]+.$"), Message.Type.ThePlayerStandUpWithTheStackOf },
            { new Regex("^The player \".* @ .*\" sit back with the stack of [0-9]+.$"), Message.Type.ThePlayerSitBackWithTheStackOf },

            // game actions
            { new Regex("^-- starting hand #[0-9]+  \\(No Limit Texas Hold'em\\) \\(dealer: \".* @ .*\"\\) --$"), Message.Type.StartingHand },
            { new Regex("^-- ending hand #[0-9]+ --$"), Message.Type.EndingHand },
            { new Regex("^\".* @ .*\" posts a small blind of [0-9]+$"), Message.Type.PostsASmallBlindOf },
            { new Regex("^\".* @ .*\" posts a big blind of [0-9]+$"), Message.Type.PostsABigBlindOf },

            // board
            { new Regex("^Flop:  \\[.*\\]$"), Message.Type.Flop },
            { new Regex("^Turn: .* \\[.*\\]$"), Message.Type.Turn },
            { new Regex("^River: .* \\[.*\\]$"), Message.Type.River },

            // player actions        
            { new Regex("^\".* @ .*\" bets [0-9]+$"), Message.Type.Bets },
            { new Regex("^\".* @ .*\" calls [0-9]+$"), Message.Type.Calls },
            { new Regex("^\".* @ .*\" calls 2 and go all in$"), Message.Type.CallsAndGoAllIn },
            { new Regex("^\".* @ .*\" checks$"), Message.Type.Checks },
            { new Regex("^\".* @ .*\" raises to [0-9]+$"), Message.Type.RaisesTo },

            // results
            { new Regex("^Player stacks: #[0-9]+ \".* @ .*\" \\([0-9]+\\).*$"), Message.Type.PlayerStacks },
            { new Regex("^\".* @ .*\" collected [0-9]+ from pot with .* \\(combination: .*\\)$"), Message.Type.CollectedFromPotWith },
            { new Regex("^\".* @ .*\" shows a .*.$"), Message.Type.ShowsA },
            { new Regex("^Uncalled bet of [0-9]+ returned to \".* @ .*\"$"), Message.Type.UncalledBetOfReturnedTo },
            { new Regex("^Your hand is .*$"), Message.Type.YourHandIs }
        };

        public Message.Type GetMessageType(string message)
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

        public int GetTotalStack(string message)
        {
            // Player stacks: #4 \"asd @ 89BxM1Wz6Q\" (122) | #6 \"Test @ 7dHPQQOOVK\" (123)
            // We're after the total of everything in brackets

            int totalStack = 0;

            Regex regex = new Regex("\\([0-9]+\\)"); // (122)

            MatchCollection matches = regex.Matches(message);

            foreach (Match match in matches)
            {
                totalStack += Int32.Parse(match.Value.Substring(1, match.Value.Length - 2));
            }

            return totalStack;
        }
    }
}
