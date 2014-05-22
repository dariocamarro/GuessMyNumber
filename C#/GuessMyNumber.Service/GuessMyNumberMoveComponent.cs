using Gamify.Contracts.Notifications;
using Gamify.Contracts.Requests;
using Gamify.Core;
using Gamify.Core.Interfaces;
using Gamify.Service;
using Gamify.Service.Components;
using GuessMyNumber.Contracts.Notifications;
using GuessMyNumber.Contracts.Requests;
using GuessMyNumber.Core;
using GuessMyNumber.Core.Game;
using GuessMyNumber.Core.Interfaces;
using System.Linq;

namespace GuessMyNumber.Service.Components
{
    public class GuessMyNumberMoveComponent : IGameComponent
    {
        private readonly ISerializer<MoveRequestObject> serializer;
        private readonly INotificationService notificationService;
        private readonly IGameController gameController;

        public GuessMyNumberMoveComponent(INotificationService notificationService, IGameController gameController)
        {
            this.serializer = new JsonSerializer<MoveRequestObject>();
            this.notificationService = notificationService;
            this.gameController = gameController;
        }

        public bool CanHandleRequest(GameRequest request)
        {
            return request.Type == (int)GameRequestType.GameMove;
        }

        public void HandleRequest(GameRequest request)
        {
            var moveRequestObject = this.serializer.Deserialize(request.SerializedRequestObject);
            var number = new Number(moveRequestObject.Number);
            var move = new GuessMyNumberMove(number);
            var moveResponse = this.gameController.HandleMove<INumber, IAttemptResult>(moveRequestObject.PlayerName, moveRequestObject.SessionId, move);
            var currentSession = this.gameController.Sessions.First(s => s.Id == moveRequestObject.SessionId);

            if (moveResponse.MoveResponseObject.IsWinner())
            {
                var winnerPlayerName = moveRequestObject.PlayerName;
                var looserPlayerName = currentSession.Player1.Information.UserName == winnerPlayerName ?
                    currentSession.Player2.Information.UserName :
                    currentSession.Player1.Information.UserName;
                var gameFinishedNotificationObject = new GameFinishedNotificationObject
                {
                    SessionId = currentSession.Id,
                    WinnerPlayerName = winnerPlayerName,
                    LooserPlayerName = looserPlayerName
                };

                this.notificationService.SendBroadcast(GameNotificationType.GameFinished, gameFinishedNotificationObject, winnerPlayerName, looserPlayerName);
                
                return;
            }

            var originPlayer = currentSession.Player1.Information.UserName == moveRequestObject.PlayerName ?
                currentSession.Player1 :
                currentSession.Player2;
            var destinationPlayer = currentSession.Player1.Information.UserName == moveRequestObject.PlayerName ?
                currentSession.Player2 :
                currentSession.Player1;

            originPlayer.NeedsToMove = false;
            destinationPlayer.NeedsToMove = true;

            this.SendMoveNotification(moveRequestObject, destinationPlayer.Information.UserName, number);
            this.SendMoveResultNotification(moveRequestObject, moveResponse, destinationPlayer.Information.UserName, number);
        }

        private void SendMoveNotification(MoveRequestObject moveRequestObject, string destinationPlayerName, INumber number)
        {
            var gameMoveNotificationObject = new MoveNotificationObject
            {
                SessionId = moveRequestObject.SessionId,
                PlayerName = moveRequestObject.PlayerName,
                Number = number.ToString()
            };

            this.notificationService.Send(GameNotificationType.GameMove, gameMoveNotificationObject, destinationPlayerName);
        }

        private void SendMoveResultNotification(MoveRequestObject moveRequestObject, IGameMoveResponse<IAttemptResult> moveResponse, string destinationPlayerName, INumber number)
        {
            var gameMoveResultNotificationObject = new MoveResultNotificationObject
            {
                SessionId = moveRequestObject.SessionId,
                PlayerName = destinationPlayerName,
                Number = number.ToString(),
                Goods = moveResponse.MoveResponseObject.Goods,
                Regulars = moveResponse.MoveResponseObject.Regulars,
                Bads = moveResponse.MoveResponseObject.Bads
            };

            this.notificationService.Send(GameNotificationType.GameMoveResult, gameMoveResultNotificationObject, moveRequestObject.PlayerName);
        }
    }
}
