// Startup.cs
using Microsoft.Owin;
using Owin;
using Unity;
using Unity.AspNet.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Data.OracleClient;
using System;
using EcommerceCoffe_DAL.Model.IdentityModel;
using Microsoft.AspNet.Identity.Owin;
[assembly: OwinStartup(typeof(EcommerceCoffee.Startup))]

namespace EcommerceCoffee
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Register Unity DI
            // UnityConfig.RegisterComponents(app);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromDays(14), // Adjust as needed
                SlidingExpiration = true,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<ApplicationUser>, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenrateUserIdentityAsync(manager))
                }
            });

            // Use session middleware
            app.Use(async (context, next) =>
            {
                // Enable session state
                context.Environment["System.Web.SessionState"] = true;
                await next.Invoke();
            });

        }
    }
}
