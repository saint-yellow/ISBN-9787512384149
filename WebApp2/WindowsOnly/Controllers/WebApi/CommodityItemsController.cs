using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WindowsOnly.Services;
using WindowsOnly.Models.DataModels;
using WindowsOnly.Models.ViewModels;

namespace WindowsOnly.Controllers.WebApi
{
    public class CommodityItemsController : ApiController
    {
        private readonly CommodityItemService _service = new CommodityItemService();
        private readonly MapperConfiguration _mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Book, BookViewModel>();
            config.CreateMap<BookViewModel, Book>();

            config.CreateMap<Author, AuthorViewModel>();
            config.CreateMap<AuthorViewModel, Author>();

            config.CreateMap<Category, CategoryViewModel>();
            config.CreateMap<CategoryViewModel, Category>();

            config.CreateMap<CommodityItem, CommodityItemViewModel>();
            config.CreateMap<CommodityItemViewModel, CommodityItem>();
        });

        public CommodityItemViewModel Post(CommodityItemViewModel commodityItem)
        {
            CommodityItem newCommodityItem = _service.AddToShoppingCart(_mapperConfig.CreateMapper().Map<CommodityItem>(commodityItem));
            return _mapperConfig.CreateMapper().Map<CommodityItemViewModel>(newCommodityItem);
        }

        public CommodityItemViewModel Put(CommodityItemViewModel commodityItem)
        {
            CommodityItem result = _mapperConfig.CreateMapper().Map<CommodityItem>(commodityItem);
            _service.UpdateInShoppingCart(result);
            return commodityItem;
        }

        public CommodityItemViewModel Delete(CommodityItemViewModel commodityItem)
        {
            CommodityItem result = _mapperConfig.CreateMapper().Map<CommodityItem>(commodityItem);
            _service.DeleteFromShoppingCart(result);
            return commodityItem;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
