using Gamify.Service;
using Gamify.WebServer.Controllers;
using GuessMyNumber.Core.Game;
using Microsoft.Web.WebSockets;

namespace GuessMyNumber.WebServer.Controllers
{
    public class GuessMyNumberApiController : GameApiController
    {
        protected override WebSocketHandler GetWebSocketHandler(string userName)
        {
            var notificationService = new NotificationService();
            var gameController = new GuessMyNumberGameController();

            return new GuessMyNumberWebSocketHandler(userName, notificationService, gameController);
        }
    }
}