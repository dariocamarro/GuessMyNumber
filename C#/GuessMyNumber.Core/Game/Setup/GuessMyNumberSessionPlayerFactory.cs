using Gamify.Sdk;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberSessionPlayerFactory : ISessionPlayerFactory
    {
        public SessionGamePlayer Create(IGamePlayer gamePlayer)
        {
            return new GuessMyNumberPlayer(gamePlayer);
        }
    }
}
