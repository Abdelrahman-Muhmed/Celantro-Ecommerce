using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee_BLL.Helpers
{
    public class CultureActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string culture = "en-US";
            var session = filterContext.HttpContext.Session;

            if (session == null || session["CurrentCulture"] == null)
            {
                var appCulture = System.Configuration.ConfigurationManager.AppSettings["Culture"];
                if (!string.IsNullOrEmpty(appCulture))
                {
                    culture = CultureHelper.GetImplementedCulture(appCulture);
                }

                if (session != null)
                {
                    session["CurrentCulture"] = culture;
                }
            }
            else
            {
                culture = session["CurrentCulture"] as string;
                if (string.IsNullOrEmpty(culture))
                {
                    culture = "en-US"; 
                }
                else
                {
                    culture = CultureHelper.GetImplementedCulture(culture);
                }
            }

            CultureHelper.CurrentCulture = culture;

           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);

            base.OnActionExecuting(filterContext);
        }
    }
}