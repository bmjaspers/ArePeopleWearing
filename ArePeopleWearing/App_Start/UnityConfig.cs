using ArePeopleWearing.Forecasts.Services;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace ArePeopleWearing
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            var forecastIOApiKey = ConfigurationManager.AppSettings["ForecastIOApiKey"];
            container.RegisterType<IForecastService, ForecastIOService>(new InjectionConstructor(new ForecastIOClient(forecastIOApiKey), new ForecastIOResponseMapper()));
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}