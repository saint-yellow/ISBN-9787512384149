using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Behaviors
{
    public class QueryOptionCalculator
    {
        public static int CalculateStart(QueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
        }

        public static int CalculateTotalPages(int count, int pageSize)
        {
            return (int)Math.Ceiling((double)count / pageSize);
        }
    }
}