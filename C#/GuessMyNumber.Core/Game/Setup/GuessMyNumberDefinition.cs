using GuessMyNumber.Core.Interfaces;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberDefinition : GameDefinition<INumber, IAttemptResult>
    {
        public override ISessionPlayerFactory GetSessionPlayerFactory()
        {
            return new GuessMyNumberSessionPlayerFactory();
        }

        public override ISessionPlayerSetup GetSessionPlayerSetup()
        {
            return new SessionPlayerSetup();
        }

        public override IMoveFactory<INumber> GetMoveFactory()
        {
            return new GuessMyNumberMoveFactory();
        }

        public override IMoveProcessor<INumber, IAttemptResult> GetMoveProcessor()
        {
            return new GuessMyNumberMoveProcessor();
        }

        public override IMoveResultNotificationFactory GetMoveResultNotificationFactory()
        {
            return new MoveResultNotificationFactory();
        }

        public override IGameInviteDecorator GetGameInviteDecorator()
        {
            return new GameInviteDecorator();
        }

        public override IGameInformationNotificationFactory<INumber, IAttemptResult> GetGameInformationNotificationFactory()
        {
            return new GameInformationNotificationFactory();
        }

        public override IPlayerHistoryItemFactory<INumber, IAttemptResult> GetPlayerHistoryItemfactory()
        {
            throw new System.NotImplementedException();
        }
    }
}
