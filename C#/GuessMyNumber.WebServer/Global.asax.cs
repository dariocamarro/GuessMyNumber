using Autofac;
using Autofac.Integration.WebApi;
using Gamify.Sdk;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Game.Setup;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GuessMyNumber.WebServer
{
    public class GuessMyNumberApplication : HttpApplication
    {
        private IContainer guessMyNumberContainer;

        protected void Application_Start()
        {
            this.ConfigureDependencies();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public override void Dispose()
        {
            this.Dispose(disposing: true);
            base.Dispose();
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.guessMyNumberContainer != null)
                {
                    this.guessMyNumberContainer.Dispose();
                }
            }
        }

        private void ConfigureDependencies()
        {
            var gameDefinition = new GuessMyNumberDefinition();
            var gameInitializer = new GameInitializer(gameDefinition);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterInstance(gameInitializer).As<IGameInitializer>();
            containerBuilder.RegisterType<JsonSerializer>().As<ISerializer>().InstancePerApiRequest();

            this.guessMyNumberContainer = containerBuilder.Build();

            var resolver = new AutofacWebApiDependencyResolver(this.guessMyNumberContainer);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}