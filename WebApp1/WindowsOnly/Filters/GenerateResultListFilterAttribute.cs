using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenerateResultListFilterAttribute : FilterAttribute, IResultFilter 
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;

        MapperConfiguration _mapperConfiguration;

        public GenerateResultListFilterAttribute(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;

            _mapperConfiguration = new MapperConfiguration(config => 
            {
                config.CreateMap(_sourceType, _destinationType);
                config.CreateMap(_destinationType, _sourceType);
            });
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            object model = filterContext.Controller.ViewData.Model;

            Type resultListGenericType = typeof(ResultList<>).MakeGenericType(new Type[] { _destinationType });

            Type sourceGenerticType = typeof(List<>).MakeGenericType(new Type[] { _sourceType });
            Type destinationGenericType = typeof(List<>).MakeGenericType(new Type[] { _destinationType });

            IMapper mapper = _mapperConfiguration.CreateMapper();
            object viewModel = mapper.Map(model, sourceGenerticType, destinationGenericType);
            QueryOptions queryOptions = filterContext.Controller.ViewData.ContainsKey("QueryOptions") ? (QueryOptions) filterContext.Controller.ViewData["QueryOptions"] : new QueryOptions();
            var resultList = Activator.CreateInstance(resultListGenericType, viewModel, queryOptions);

            filterContext.Controller.ViewData.Model = resultList;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
    }
}