using System.Text.RegularExpressions;

namespace PNP.Services
{
    public class CardService
    {
        public enum Face
        {
            None,
            Ace,
            Jack,
            Queen,
            King
        }

        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public Suit GetSuit(string card)
        {
            switch (card.Substring(card.Length - 1))
            {
                case "♣": return Suit.Clubs;
                case "♦": return Suit.Diamonds;
                case "♥": return Suit.Hearts;
                case "♠": return Suit.Spades;
                default: throw new NotImplementedException();
            }
        }

        public Face GetFace(string card)
        {
            switch (card.Substring(0, card.Length - 1))
            {
                case "A": return Face.Ace;
                case "J": return Face.Jack;
                case "Q": return Face.Queen;
                case "K": return Face.King;
                default: return Face.None;
            }
        }

        public bool IsFaceCard(string card)
        {
            Regex regex = new Regex("^[AJQK][♣♦♥♠]$");

            return regex.IsMatch(card);
        }
    }
}
