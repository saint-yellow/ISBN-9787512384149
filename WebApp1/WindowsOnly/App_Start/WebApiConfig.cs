using System.Web.Http;
using WindowsOnly.Filters;

namespace WindowsOnly
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidationActionFilterAttribute());
            config.Filters.Add(new OnWebApiExceptionFilterAttribute());


            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}