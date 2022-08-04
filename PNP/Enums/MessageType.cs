namespace PNP.Enums
{
    public class Message
    {
        public enum Type
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
    }
}
