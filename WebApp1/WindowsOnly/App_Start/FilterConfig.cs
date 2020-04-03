using System.Web;
using System.Web.Mvc;
using WindowsOnly.Filters;

namespace WindowsOnly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OnMvcExceptionAttribute());
            filters.Add(new BasicAuthenticationFilterAttribute());
        }
    }
}
