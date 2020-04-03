using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WindowsOnly.Models;

namespace WindowsOnly.Filters
{
    public class BasicAuthenticationFilterAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            string authorization = request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization) || !authorization.Contains("Basic"))
            {
                return;
            }

            byte[] encodedData = Convert.FromBase64String(authorization.Replace("Basic", ""));
            string value = Encoding.ASCII.GetString(encodedData);

            string username = value.Substring(0, value.IndexOf(':'));
            string password = value.Substring(value.IndexOf(':') + 1);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                filterContext.Result = new HttpUnauthorizedResult("Username of password missing.");
                return;
            }

            User user = AuthenticatedUsers.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
            if (user == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("Invalid username and password");
                return;
            }

            filterContext.Principal = new GenericPrincipal(user, user.Roles);
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            filterContext.Result = new BasicChallengeActionResult
            {
                CurrentResult = filterContext.Result
            };
        }
    }

    class BasicChallengeActionResult : ActionResult
    {
        public ActionResult CurrentResult { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            CurrentResult.ExecuteResult(context);

            HttpResponseBase response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response.AddHeader("WWW-Authenticate", "Basic");
            }
        }
    }
}