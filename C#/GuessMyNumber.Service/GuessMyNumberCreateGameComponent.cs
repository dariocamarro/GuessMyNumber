using Gamify.Contracts.Notifications;
using Gamify.Contracts.Requests;
using Gamify.Core.Interfaces;
using Gamify.Service;
using Gamify.Service.Components;
using GuessMyNumber.Core;
using GuessMyNumber.Core.Game;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.Service.Components
{
    public class GuessMyNumberCreateGameComponent : CreateGameComponent
    {
        public GuessMyNumberCreateGameComponent(INotificationService notificationService, IGameController gameController)
            : base(notificationService, gameController)
        {
        }

        protected override IEnumerable<ISessionGamePlayerBase> GetSessionPlayers(CreateGameRequestObject createGameRequestObject)
        {
            var connectedPlayer1 = this.gameController.Players.First(p => p.UserName == createGameRequestObject.PlayerName);
            var sessionPlayer1 = new GuessMyNumberPlayer(connectedPlayer1);
            var connectedPlayer2 = this.gameController.Players.First(p => p.UserName == createGameRequestObject.InvitedPlayerName);
            var sessionPlayer2 = new GuessMyNumberPlayer(connectedPlayer2);

            var playerNumber = new Number(createGameRequestObject.AdditionalInformation);

            sessionPlayer1.AssignNumber(playerNumber);

            return new List<ISessionGamePlayerBase> { sessionPlayer1, sessionPlayer2 };
        }

        protected override void DecorateGameInvitation(GameInviteNotificationObject gameInviteNotificationObject)
        {
            var currentSession = this.gameController.Sessions.First(s => s.Id == gameInviteNotificationObject.SessionId);
            var sessionPlayer1 = currentSession.Player1 as GuessMyNumberPlayer;

            gameInviteNotificationObject.AdditionalInformation = sessionPlayer1.Number.ToString();
        }
    }
}
