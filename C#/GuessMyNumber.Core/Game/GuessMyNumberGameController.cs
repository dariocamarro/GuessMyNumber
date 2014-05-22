using Gamify.Core;
using Gamify.Core.Interfaces;

namespace GuessMyNumber.Core.Game
{
    public class GuessMyNumberGameController : GameController
    {
        protected override ISessionGamePlayerBase GetSessionPlayer(IGamePlayer player)
        {
            return new GuessMyNumberPlayer(player);
        }
    }
}
