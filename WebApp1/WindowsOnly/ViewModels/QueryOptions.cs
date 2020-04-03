using Newtonsoft.Json;
using System.ComponentModel;

namespace WindowsOnly.ViewModels
{
    [JsonObject]
    public class QueryOptions
    {
        public QueryOptions()
        {
            CurrentPage = 1;
            PageSize = 2;

            SortField = "Id";
            SortOrder = SortOrder.ASC;
        }

        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "sortField")]
        public string SortField { get; set; }

        [JsonProperty(PropertyName = "sortOrder")]
        public SortOrder SortOrder { get; set; }

        public string Sort
        {
            get
            {
                return $"{SortField} {SortOrder}";
            }
        }
    }

    public enum SortOrder
    {
        [Description("ASC")]
        ASC,

        [Description("DESC")]
        DESC
    }
}