using Gamify.Sdk;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class SessionPlayerFactory : ISessionPlayerFactory<INumber, IAttemptResult>
    {
        public SessionGamePlayer<INumber, IAttemptResult> Create(IGamePlayer gamePlayer, ISessionHistoryService<INumber, IAttemptResult> sessionHistoryService)
        {
            return new GuessMyNumberPlayer(gamePlayer, sessionHistoryService);
        }

        ISessionGamePlayerBase ISessionPlayerFactory.Create(IGamePlayer gamePlayer, ISessionHistoryService sessionHistoryService)
        {
            return this.Create(gamePlayer, sessionHistoryService as ISessionHistoryService<INumber, IAttemptResult>);
        }
    }
}
