using Gamify.Sdk.Setup;
using Gamify.WebServer;
using GuessMyNumber.Core.Game.Setup;

namespace GuessMyNumber.WebServer
{
    public class MvcApplication : GamifyApplication
    {
        protected override IGameDefinition GetGameDefinition()
        {
            return new GuessMyNumberDefinition();
        }
    }
}