using Gamify.Contracts.Requests;
using Gamify.Core.Interfaces;
using Gamify.Service;
using Gamify.Service.Components;
using GuessMyNumber.Core;
using GuessMyNumber.Core.Game;
using System.Linq;

namespace GuessMyNumber.Service.Components
{
    public class GuessMyNumberAcceptGameComponent : AcceptGameComponent
    {
        public GuessMyNumberAcceptGameComponent(INotificationService notificationService, IGameController gameController)
            : base(notificationService, gameController)
        {
        }

        protected override void GetSessionPlayer2Ready(GameAcceptedRequestObject gameAcceptedRequestObject)
        {
            var currentSession = this.gameController.Sessions.First(s => s.Id == gameAcceptedRequestObject.SessionId);
            var sessionPlayer2 = currentSession.Player2 as GuessMyNumberPlayer;
            var player2Number = new Number(gameAcceptedRequestObject.AdditionalInformation);

            sessionPlayer2.AssignNumber(player2Number);
        }
    }
}
