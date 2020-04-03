using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsOnly;
using WindowsOnly.Models.DataModels;
using WindowsOnly.Models.ViewModels;
using WindowsOnly.Services;

namespace WindowsOnly.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ShoppingCartService _service = new ShoppingCartService();
        private readonly MapperConfiguration _mapperConfig = new MapperConfiguration(config => 
        {
            config.CreateMap<ShoppingCart, ShoppingCartViewModel>();
            config.CreateMap<ShoppingCartViewModel, ShoppingCart>();

            config.CreateMap<CommodityItem, CommodityItemViewModel>();
            config.CreateMap<CommodityItemViewModel, CommodityItem>();

            config.CreateMap<Book, BookViewModel>();
            config.CreateMap<BookViewModel, Book>();

            config.CreateMap<Author, AuthorViewModel>();
            config.CreateMap<AuthorViewModel, Author>();

            config.CreateMap<Category, CategoryViewModel>();
            config.CreateMap<CategoryViewModel, Category>();
        });

        public ActionResult Index()
        {
            ShoppingCart shoppingCart = _service.GetBySessionId(HttpContext.Session.SessionID);
            ShoppingCartViewModel result = _mapperConfig.CreateMapper().Map<ShoppingCartViewModel>(shoppingCart);
            return View(result);
        }

        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            ShoppingCart shoppingCart = _service.GetBySessionId(HttpContext.Session.SessionID);
            ShoppingCartViewModel shoppingCartViewModel = _mapperConfig.CreateMapper().Map<ShoppingCartViewModel>(shoppingCart);
            return PartialView(shoppingCartViewModel);
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