using EcommerceCoffee_BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language/SetLanguage
        public ActionResult SetLanguage(string culture)
        {

            culture = CultureHelper.GetImplementedCulture(culture);


            Session["CurrentCulture"] = culture;

            string returnUrl = Request.UrlReferrer?.AbsolutePath ?? "~/";
            return Redirect(returnUrl);
        }
    }
}