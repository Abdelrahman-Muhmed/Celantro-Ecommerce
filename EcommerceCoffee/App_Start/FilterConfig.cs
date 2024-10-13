using EcommerceCoffee_BLL.Helpers;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureActionFilter()); // Register the custom culture filter
        }
    }
}
