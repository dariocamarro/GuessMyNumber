using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GameInformationNotificationFactory : IGameInformationNotificationFactory
    {
        public GameInformationNotificationObject Create(IGameSession session)
        {
            var sessionPlayer1 = session.Player1 as GuessMyNumberPlayer;
            var sessionPlayer2 = session.Player2 as GuessMyNumberPlayer;
            var sessionPlayer1History = new PlayerHistoryObject(sessionPlayer1.Information.Name);
            var sessionPlayer2History = new PlayerHistoryObject(sessionPlayer2.Information.Name);

            var sessionPlayer2Moves = sessionPlayer1.GetHistory().Get();

            foreach (var sessionPlayer2Move in sessionPlayer2Moves)
            {
                sessionPlayer2History.AddMove(new PlayerHistoryItemObject
                {
                    Number = sessionPlayer2Move.Response.Number.ToString(),
                    Goods = sessionPlayer2Move.Response.Goods,
                    Regulars = sessionPlayer2Move.Response.Regulars,
                    Bads = sessionPlayer2Move.Response.Bads
                });
            }

            var sessionPlayer1Moves = sessionPlayer2.GetHistory().Get();

            foreach (var sessionPlayer1Move in sessionPlayer1Moves)
            {
                sessionPlayer1History.AddMove(new PlayerHistoryItemObject
                {
                    Number = sessionPlayer1Move.Response.Number.ToString(),
                    Goods = sessionPlayer1Move.Response.Goods,
                    Regulars = sessionPlayer1Move.Response.Regulars,
                    Bads = sessionPlayer1Move.Response.Bads
                });
            }

            var gameInformationNotificationObject = new GameInformationNotificationObject
            {
                SessionName = session.Name,
                Player1History = sessionPlayer1History,
                Player2History = sessionPlayer2History
            };

            return gameInformationNotificationObject;
        }
    }
}
