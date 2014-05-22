using Gamify.Contracts.Notifications;
using Gamify.Core.Interfaces;
using Gamify.Service;
using Gamify.Service.Components;
using GuessMyNumber.Contracts.Notifications;
using GuessMyNumber.Core.Game;

namespace GuessMyNumber.Service.Components
{
    public class GuessMyNumberOpenGameComponent : OpenGameComponent
    {
        public GuessMyNumberOpenGameComponent(INotificationService notificationService, IGameController gameController)
            : base(notificationService, gameController)
        {
        }

        protected override void SendGameInformation(string playerName, IGameSession gameSession)
        {
            var sessionPlayer1 = gameSession.Player1 as GuessMyNumberPlayer;
            var sessionPlayer2 = gameSession.Player2 as GuessMyNumberPlayer;
            var sessionPlayer1History = new PlayerHistoryObject(sessionPlayer1.Information.UserName);
            var sessionPlayer2History = new PlayerHistoryObject(sessionPlayer2.Information.UserName);

            foreach (var player2Move in sessionPlayer1.MovesHistory.Moves)
            {
                sessionPlayer2History.AddMove(new PlayerHistoryItemObject
                {
                    Number = player2Move.Response.Number.ToString(),
                    Goods = player2Move.Response.Goods,
                    Regulars = player2Move.Response.Regulars,
                    Bads = player2Move.Response.Bads
                });
            }

            foreach (var player1Move in sessionPlayer2.MovesHistory.Moves)
            {
                sessionPlayer1History.AddMove(new PlayerHistoryItemObject
                {
                    Number = player1Move.Response.Number.ToString(),
                    Goods = player1Move.Response.Goods,
                    Regulars = player1Move.Response.Regulars,
                    Bads = player1Move.Response.Bads
                });
            }

            var gameInformationNotificationObject = new GameInformationNotificationObject
            {
                SessionId = gameSession.Id,
                Player1History = sessionPlayer1History,
                Player2History = sessionPlayer2History
            };

            this.notificationService.Send(GameNotificationType.SendGameInformation, gameInformationNotificationObject, playerName);
        }
    }
}
