using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberInviteDecorator : IGameInviteDecorator
    {
        public void Decorate(GameInviteNotificationObject gameInviteNotification, IGameSession session)
        {
            var sessionPlayer1 = session.Player1 as GuessMyNumberPlayer;

            gameInviteNotification.AdditionalInformation = sessionPlayer1.Number.ToString();
        }
    }
}
