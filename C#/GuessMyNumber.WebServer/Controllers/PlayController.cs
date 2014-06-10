using Gamify.Sdk;
using Gamify.Sdk.Setup;
using Microsoft.Web.WebSockets;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuessMyNumber.WebServer.Controllers
{
    public class PlayController : ApiController
    {
        private readonly IGameInitializer gameInitializer;
        private ISerializer serializer;

        public PlayController(IGameInitializer gameInitializer, ISerializer serializer)
        {
            this.gameInitializer = gameInitializer;
            this.serializer = serializer;
        }

        public HttpResponseMessage Get()
        {
            var responseCode = HttpStatusCode.BadRequest;

            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(this.GetWebSocketHandler());
                responseCode = HttpStatusCode.SwitchingProtocols;
            }

            return new HttpResponseMessage(responseCode);
        }

        private WebSocketHandler GetWebSocketHandler()
        {
            var gameWebSocketHandler = new GameWebSocketHandler(this.gameInitializer, this.serializer);

            return gameWebSocketHandler;
        }
    }
}