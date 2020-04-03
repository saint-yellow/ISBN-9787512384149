using System.Web.Mvc;
using WindowsOnly.Models;

namespace WindowsOnly.Filters
{
    public class BasicAuthorizationFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            User user = filterContext.HttpContext.User.Identity as User;
            if (user == null || !user.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            string visitorIpAddress = filterContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(visitorIpAddress))
            {
                visitorIpAddress = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(visitorIpAddress))
            {
                visitorIpAddress = filterContext.HttpContext.Request.UserHostAddress;
            }
            if (user.ValidIpAddress != null && !user.ValidIpAddress.Contains(visitorIpAddress))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
        }
    }
}