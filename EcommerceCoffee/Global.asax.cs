using EcommerceCoffe_DAL.Model.IdentityModel;
using EcommerceCoffe_DAL.Prsistence.Data;
using EcommerceCoffee.Controllers;
using EcommerceCoffee_BLL.Service.IAuthServ;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.Owin;
using Owin;
using System;
using AutoMapper;
using EcommerceCoffee_BLL.Helpers.Mapping;
using System.Web.Security;
using Microsoft.AspNetCore.Identity;

namespace EcommerceCoffee
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



            //// Register Unity
            UnityConfig.RegisterComponents();


        }
       
        //Handle Automatic LogOut 
        protected void Session_End(object sender, EventArgs e)
        {
            var userId = Session["UserId"] as string;
            if (!string.IsNullOrEmpty(userId))
            {
               var userManager = DependencyResolver.Current.GetService<Microsoft.AspNet.Identity.UserManager<ApplicationUser>>();

                var user = userManager.FindById(userId);
                if (user != null && user.CurrentSessionId == Session.SessionID)
                {
                    user.IsLoggedIn = false;
                    user.CurrentSessionId = null;
                    userManager.Update(user);
                }
            }
        }
    }
}
