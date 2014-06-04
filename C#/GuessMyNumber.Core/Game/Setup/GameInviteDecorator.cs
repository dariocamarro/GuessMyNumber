using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Setup;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GameInviteDecorator : IGameInviteDecorator
    {
        public void Decorate(GameInviteNotificationObject gameInviteNotification, IGameSession session)
        {
            var sessionPlayer1 = session.Player1 as GuessMyNumberPlayer;

            gameInviteNotification.AdditionalInformation = sessionPlayer1.Number.ToString();
        }
    }
}
