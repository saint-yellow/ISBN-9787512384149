using Newtonsoft.Json;
using System.Collections.Generic;

namespace WindowsOnly.ViewModels
{
    public class ResultList<T>
    {
        public ResultList(List<T> results, QueryOptions queryOptions)
        {
            Results = results;
            QueryOptions = queryOptions;
        }

        [JsonProperty(PropertyName = "queryOptions")]
        public QueryOptions QueryOptions { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<T> Results { get; set; }
    }
}