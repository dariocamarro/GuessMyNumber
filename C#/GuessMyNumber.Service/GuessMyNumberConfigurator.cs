using Gamify.Core.Interfaces;
using Gamify.Service;
using GuessMyNumber.Service.Components;

namespace GuessMyNumber.Service
{
    public class GuessMyNumberConfigurator : IGameConfigurator
    {
        private readonly INotificationService notificationService;
        private readonly IGameController gameController;

        public GuessMyNumberConfigurator(INotificationService notificationService, IGameController gameController)
        {
            this.notificationService = notificationService;
            this.gameController = gameController;
        }

        public void Configure(IGameServiceSetup gameServiceSetup)
        {
            gameServiceSetup.RegisterComponent(new GuessMyNumberAcceptGameComponent(this.notificationService, this.gameController));
            gameServiceSetup.RegisterComponent(new GuessMyNumberCreateGameComponent(this.notificationService, this.gameController));
            gameServiceSetup.RegisterComponent(new GuessMyNumberMoveComponent(this.notificationService, this.gameController));
            gameServiceSetup.RegisterComponent(new GuessMyNumberOpenGameComponent(this.notificationService, this.gameController));
        }
    }
}
