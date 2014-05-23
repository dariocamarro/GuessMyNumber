using Gamify.Core.Interfaces;
using Gamify.Service;
using Gamify.WebServer;
using GuessMyNumber.Service;
using System.Collections.Generic;

namespace GuessMyNumber.WebServer
{
    public class GuessMyNumberWebSocketHandler : GameWebSocketHandler
    {
        private readonly INotificationService notificationService;
        private readonly IGameController gameController;

        public GuessMyNumberWebSocketHandler(string userName, INotificationService notificationService, IGameController gameController)
            : base(userName)
        {
            this.notificationService = notificationService;
            this.gameController = gameController;

            this.Initialize(new GameService(this.notificationService, this.gameController));
        }

        protected override IEnumerable<IGameConfigurator> GetGameConfigurators()
        {
            var gameConfigurators = new List<IGameConfigurator>();

            gameConfigurators.Add(new GamifyConfigurator(this.notificationService, this.gameController));
            gameConfigurators.Add(new GuessMyNumberConfigurator(this.notificationService, this.gameController));

            return gameConfigurators;
        }
    }
}