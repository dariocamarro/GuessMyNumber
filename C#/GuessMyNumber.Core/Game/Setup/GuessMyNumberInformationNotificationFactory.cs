using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup.Definition;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberInformationNotificationFactory : IGameInformationNotificationFactory<INumber, IAttemptResult>
    {
        public GameInformationNotificationObject Create(IGameSession session, ISessionHistoryService<INumber, IAttemptResult> sessionHistoryService, 
            IPlayerHistoryItemFactory<INumber, IAttemptResult> playerHistoryItemFactory)
        {
            var sessionPlayer1 = session.Player1;
            var sessionPlayer2 = session.Player2;
            var sessionPlayer1History = new PlayerHistoryObject(sessionPlayer1.Information.Name);
            var sessionPlayer2History = new PlayerHistoryObject(sessionPlayer2.Information.Name);

            var sessionPlayer1Moves = sessionHistoryService.GetBySessionPlayer(session.Name, sessionPlayer1.Information.Name).Get();

            foreach (var sessionPlayer1Move in sessionPlayer1Moves)
            {
                var historiItem = playerHistoryItemFactory.Create(sessionPlayer1Move.Response);

                sessionPlayer1History.AddMove(historiItem);
            }

            var sessionPlayer2Moves = sessionHistoryService.GetBySessionPlayer(session.Name, sessionPlayer2.Information.Name).Get();

            foreach (var sessionPlayer2Move in sessionPlayer2Moves)
            {
                var historiItem = playerHistoryItemFactory.Create(sessionPlayer2Move.Response);

                sessionPlayer2History.AddMove(historiItem);
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
