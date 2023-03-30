using PNP.Models;
using System.Text.RegularExpressions;

namespace PNP.Services
{
    // purpose of the hand service is to sift through the logs and identify the start and end point of a hand
    // then parse the inbetween bits into something useful.

    public static class HandService
    {
        public static Hand GetHand(ushort handNumber, JSON json)
        {
            Hand hand = new(handNumber);

            IEnumerable<JSON.Log> startingHandLogs = MessageService.GetLogsByMessageType(MessageService.MessageType.StartingHand, json);
            IEnumerable<JSON.Log> endingHandLogs = MessageService.GetLogsByMessageType(MessageService.MessageType.EndingHand, json);

            Regex startingHandRegex = new Regex($"^-- starting hand #{handNumber}  \\(No Limit Texas Hold'em\\) \\(dealer: \".* @ .*\"\\) --$");
            Regex endingHandRegex = new Regex($"^-- ending hand #{handNumber} --$");

            foreach (JSON.Log log in startingHandLogs)
            {
                if (startingHandRegex.IsMatch(log.Message))
                {
                    // found start of hand, start stripping info out of it

                    hand.StartedAt = log.At;
                    break;
                }
            }

            foreach (JSON.Log log in endingHandLogs)
            {
                if (endingHandRegex.IsMatch(log.Message))
                {
                    // found end of hand, start stripping info out of it

                    hand.EndedAt = log.At;
                    break;
                }
            }

            return hand;
        }

        public static IEnumerable<Hand> GetHands(JSON json, ushort numberOfHands)
        {
            ICollection<Hand> hands = new List<Hand>();

            for (int i = 1; i < numberOfHands + 1; i++)
            {
                hands.Add(GetHand((ushort)i, json));
            }

            return hands;
        }
    }
}
