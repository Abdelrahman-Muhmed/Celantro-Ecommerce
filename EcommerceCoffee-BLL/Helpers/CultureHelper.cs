using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace EcommerceCoffee_BLL.Helpers
{
    public class CultureHelper
    {
        //protected HttpSessionState _httpSessionState;
        //public CultureHelper(HttpSessionState httpSessionState)
        //{
        //    _httpSessionState = httpSessionState;
        //}

        //public static int CurrentCulture
        //{

        //    get
        //    {

        //        if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
        //            return 0;
        //        else if (Thread.CurrentThread.CurrentUICulture.Name == "ar-SA")
        //            return 1;
        //        else
        //            return 0;

        //    }
        //    set
        //    {
        //        if(value == 0)
        //            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        //        else if(value == 0)
        //            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-SA");
        //        else
        //        {
        //            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture; ///??????????????/
        //        }
        //    }

        //}

        private static readonly List<string> _cultures = new List<string> { "en-US", "ar-SA" };

        public static string GetImplementedCulture(string culture)
        {
           
            if (_cultures.Contains(culture))
                return culture;

            return "en";
        }
        public static string CurrentCulture { get; set; }

    }
}