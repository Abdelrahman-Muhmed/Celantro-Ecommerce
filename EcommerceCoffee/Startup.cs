// Startup.cs
using Microsoft.Owin;
using Owin;
using Unity;
using Unity.AspNet.Mvc;

[assembly: OwinStartup(typeof(EcommerceCoffee.Startup))]

namespace EcommerceCoffee
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Register Unity DI
            //UnityConfig.RegisterComponents(app);

            // Configure other OWIN middleware here if needed
        }
    }
}
