using EcommerceCoffee_BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Util;

namespace EcommerceCoffee.Controllers
{
    public class LanguageController : Controller
    {
        //[HttpPost]// GET: Language/SetLanguage
        //public JsonResult SetLanguageAndTranslate(string culture, string[] keys)
        //{
        //    HttpCookie cookie = new HttpCookie("Language");
        //    cookie.Value = culture;
        //    cookie.Expires = DateTime.Now.AddMonths(1);
        //    Response.Cookies.Add(cookie);

        //    //HttpContext.Current.Response.Cookies.Add(cookie);

        //    var translation = keys.ToDictionary(
        //        key => key,
        //        key => HttpContext.GetGlobalResourceObject("Resources", key, new System.Globalization.CultureInfo(culture)) as string


        //        );
        //    return Json(new { success = true, translations = translation });
        //}

        [HttpGet]
        public ActionResult SetLanguage(string culture)
        {
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = culture;
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);

            culture = CultureHelper.GetImplementedCulture(culture);


            Session["CurrentCulture"] = culture;

            string returnUrl = Request.UrlReferrer?.AbsolutePath ?? "~/";
            return Redirect(returnUrl);
        }
    }
}