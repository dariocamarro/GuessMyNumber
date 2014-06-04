using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberDefinition : GameDefinition<INumber, IAttemptResult>
    {
        public override IGameInformationNotificationFactory GetGameInformationNotificationFactory()
        {
            return new GameInformationNotificationFactory();
        }

        public override IMoveHandler GetMoveHandler()
        {
            return new GuessMyNumberMoveHandler();
        }

        public override IMoveResultNotificationFactory GetMoveResultNotificationFactory()
        {
            return new MoveResultNotificationFactory();
        }

        public override ISessionPlayerFactory<INumber, IAttemptResult> GetSessionPlayerFactory()
        {
            return new SessionPlayerFactory();
        }

        public override IGameInviteDecorator GetGameInviteDecorator()
        {
            return new GameInviteDecorator();
        }

        public override ISessionPlayerSetup GetSessionPlayerSetup()
        {
            return new SessionPlayerSetup();
        }
    }
}
