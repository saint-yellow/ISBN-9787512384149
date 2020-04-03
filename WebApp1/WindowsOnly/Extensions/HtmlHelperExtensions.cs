using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WindowsOnly.ViewModels;

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

        public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField, QueryOptions queryOptions)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            bool isCurrentSortField = queryOptions.SortField == sortField;

            return new MvcHtmlString(string.Format("<a href=\"{0}\">{1} {2}</a>",
                urlHelper.Action(
                    actionName, 
                    new 
                    { 
                        SortField = sortField, 
                        SortOrder = (isCurrentSortField && queryOptions.SortOrder == SortOrder.ASC) ? SortOrder.DESC : SortOrder.ASC 
                    }
                ), 
                fieldName, 
                BuildSortIcon(isCurrentSortField, queryOptions)));
        }

        public static string BuildSortIcon(bool isCurrentSortField, QueryOptions queryOptions)
        {
            string sortIcon = "sort";

            if (isCurrentSortField)
            {
                sortIcon += "-by-alphabet";

                if (queryOptions.SortOrder == SortOrder.DESC)
                {
                    sortIcon += "-alt";
                }
            }

            return $"<span class=\"glyphicon glyphicon-{sortIcon}\"></span>";
        }

        public static MvcHtmlString BuildNavigationLinks(this HtmlHelper htmlHelper, QueryOptions queryOptions, string actionName)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return new MvcHtmlString(string.Format(
                "<nav>" +
                "   <ul class=\"pager\">" +
                "       <li class=\"previous {0}\">{1}</li>" +
                "       <li class=\"next {2}\">{3}</li>" +
                "   </ul>" +
                "</nav>",
                IsPreviousLinkDisabled(queryOptions),
                BuildNavigationLink(urlHelper, queryOptions, actionName, "previous"),
                IsNextLinkDisabled(queryOptions),
                BuildNavigationLink(urlHelper, queryOptions, actionName, "next")
            ));
        }

        private static string IsLinkDisabled(QueryOptions queryOptions)
        {
            if (queryOptions.CurrentPage == 1 || queryOptions.CurrentPage == queryOptions.TotalPages)
            {
                return "disabled";
            } else
            {
                return string.Empty;
            }
        }

        private static string BuildNavigationLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName, string navigationOrder)
        {
            var validOrders = new List<string> { "previous", "next" };

            if (!validOrders.Contains(navigationOrder.ToLower()))
            {
                throw new System.Exception("Unsupported Navigaton Order");
            }
            else
            {
                if (navigationOrder.ToLower() == validOrders[0])
                {
                    return string.Format(
                        "<a href=\"{0}\"><span aria-hidden=\"true\">&larr;</span> Previous</a>",
                        urlHelper.Action(actionName, new
                        {
                            queryOptions.SortOrder,
                            queryOptions.SortField,
                            CurrentPage = queryOptions.CurrentPage - 1,
                            queryOptions.PageSize
                        })
                    );

                } 
                else 
                {
                    return string.Format(
                        "<a href=\"{0}\">Next <span aria-hidden=\"true\">&rarr;</span></a>",
                        urlHelper.Action(actionName, new
                        {
                            queryOptions.SortOrder,
                            queryOptions.SortField,
                            CurrentPage = queryOptions.CurrentPage + 1,
                            queryOptions.PageSize
                        })
                    );
                }
            }

        }


        private static string IsPreviousLinkDisabled(QueryOptions queryOptions)
        {
            return queryOptions.CurrentPage == 1 ? "disabled" : string.Empty;
        }

        private static string IsNextLinkDisabled(QueryOptions queryOptions)
        {
            return queryOptions.CurrentPage == queryOptions.TotalPages ? "disabled" : string.Empty;
        }

        private static string BuildPreviousLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
        {
            return string.Format(
                "<a href=\"{0}\"><span aria-hidden=\"true\">&larr;</span> Previous</a>",
                urlHelper.Action(actionName, new
                {
                    queryOptions.SortOrder,
                    queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage - 1,
                    queryOptions.PageSize
                })
            );
        }

        private static string BuildNaxtLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
        {
            return string.Format(
                "<a href=\"{0}\">Next <span aria-hidden=\"true\">&rarr;</span></a>",
                urlHelper.Action(actionName, new 
                {
                    queryOptions.SortOrder,
                    queryOptions.SortField,
                    CurrentPage = queryOptions.CurrentPage + 1,
                    queryOptions.PageSize
                })
            );
        }
    }
}