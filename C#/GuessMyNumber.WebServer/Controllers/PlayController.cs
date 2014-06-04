using Gamify.Sdk.Setup;
using Gamify.WebServer.Controllers;
using GuessMyNumber.Core.Game.Setup;

namespace GuessMyNumber.WebServer.Controllers
{
    public class PlayController : GameApiController
    {
        protected override IGameDefinition GetGameDefinition()
        {
            return new GuessMyNumberDefinition();
        }
    }
}