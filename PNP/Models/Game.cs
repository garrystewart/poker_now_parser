using PNP.Services;

namespace PNP.Models
{
    public class Game
    {
        private readonly MessageService _messageService;

        public Game(MessageService messageService, JSON json)
        {
            _messageService = messageService;

            foreach (JSON.Log log in json.Logs)
            {
                if (_messageService.GetMessageType(log.Message) == Enums.Message.Type.PlayerStacks)
                {
                    ChipsInPlay = _messageService.GetTotalStack(log.Message);
                }
            }
        }

        public string? Id { get; set; }
        public DateTime StartedAt { get; set; }


        public IEnumerable<Player>? AllPlayers { get; set; }
        public IEnumerable<Player>? PlayersRequestingASeat { get; set; }
        public IEnumerable<Player>? PlayersApproved { get; set; }
        public IEnumerable<Player>? PlayersStoodUp { get; set; }
        public IEnumerable<Player>? PlayersSatDown { get; set; }


        public Player? Dealer { get; set; }
        public Player? SmallBlind { get; set; }
        public Player? BigBlind { get; set; }


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