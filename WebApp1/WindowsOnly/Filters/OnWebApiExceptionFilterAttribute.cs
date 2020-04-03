using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Filters
{
    public class OnWebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionType = actionExecutedContext.Exception.GetType().Name;

            ReturnData returnData;

            switch (exceptionType)
            {
                case "ObjectNotFoundException":
                    returnData = new ReturnData(HttpStatusCode.NotFound, actionExecutedContext.Exception.Message, "Error");
                    break;
                default:
                    returnData = new ReturnData(HttpStatusCode.InternalServerError, "An error occured, please try again or contact the administrator.", "Error");
                    break;
            }

            actionExecutedContext.Response = new HttpResponseMessage(returnData.HttpStatusCode)
            {
                Content = new StringContent(returnData.Content),
                ReasonPhrase = returnData.ReasonPhrase
            };
        }
    }
}