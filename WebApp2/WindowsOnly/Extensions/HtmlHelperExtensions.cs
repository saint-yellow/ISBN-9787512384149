using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace WindowsOnly.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString BuildKnockoutSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return new MvcHtmlString(
                string.Format(
                    "<div style=\"display: inline-block; margin-right: 1px;\">" +
                    "    <a href=\"{0}\" data-bind=\"click: pagingService.sortEntitiesBy\" data-sort-field=\"{1}\">" +
                    "        {2}  " +
                    "    </a>" +
                    "</div>" + 
                    "<div style=\"display: inline-block; margin-left: 1px\">" +
                    "    <span data-bind=\"css: pagingService.buildSortIcon('{1}')\"></span>" +
                    "</div>", 
                    urlHelper.Action(actionName), sortField, fieldName
                )
            );
        }

        public static MvcHtmlString BuildKnockoutNavigationLinks(this HtmlHelper htmlHelper, string actionName)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return new MvcHtmlString(
                string.Format(
                    "<nav>" + 
                    "    <ul class=\"pager\">" +
                    "        <li data-bind=\"css: pagingService.buildPreviousClass()\">" +
                    "            <a href=\"{0}\" data-bind=\"click: pagingService.previousPage\">" +
                    "                Previous" +
                    "            </a>" +
                    "        </li>" +
                    "        <li data-bind=\"css: pagingService.buildNextClass()\">" +
                    "            <a href=\"{0}\" data-bind=\"click: pagingService.nextPage\">" +
                    "                Next" +
                    "            </a>" +
                    "        </li>" +
                    "    </ul>" +
                    "</nav>", 
                    urlHelper.Action(actionName)
                )
            );
        }

        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }
    }
}