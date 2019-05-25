using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Collections.Generic;
using System.Web.Http;
using ta9.Core;
using ta9.Core.interfaces;
using ta9.Models;
using ta9.Unity;
using Unity;
using Unity.WebApi;

[assembly: OwinStartup(typeof(ta9.Startup))]

namespace ta9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            RegisterComponents();

            app.MapSignalR();
        }

        private void RegisterComponents()
        {
            var container = new UnityContainer();

            var unityHubActivator = new UnityHubActivator(container);
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDataStorageService<List<Client>>, CacheDataStorageService<List<Client>>>();
            container.RegisterType<IClientService, ClientService>();

            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => unityHubActivator);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}