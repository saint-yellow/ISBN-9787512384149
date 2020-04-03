using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsOnly.Services;
using WindowsOnly.DAL;
using WindowsOnly.Models.DataModels;
using WindowsOnly.Models.ViewModels;

namespace WindowsOnly.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _service = new BookService();
        private readonly MapperConfiguration _mapperConfig = new MapperConfiguration(config => 
        {
            config.CreateMap<Book, BookViewModel>();
            config.CreateMap<BookViewModel, Book>();

            config.CreateMap<Author, AuthorViewModel>();
            config.CreateMap<AuthorViewModel, Author>();

            config.CreateMap<Category, CategoryViewModel>();
            config.CreateMap<CategoryViewModel, Category>();
        });

        public ActionResult Index(int categoryId)
        {
            List<Book> books = _service.GetByCategoryId(categoryId);
            ViewBag.SelectedCategoryId = categoryId;
            List<BookViewModel> result = _mapperConfig.CreateMapper().Map<List<Book>, List<BookViewModel>>(books);
            return View(result);
        }

        public ActionResult Details(int id)
        {
            Book book = _service.GetById(id);
            BookViewModel result = _mapperConfig.CreateMapper().Map<BookViewModel>(book);
            return View(result);
        }

        [ChildActionOnly]
        public PartialViewResult Featured()
        {
            List<Book> books = _service.GetFeatured();
            List<BookViewModel> result = _mapperConfig.CreateMapper().Map<List<Book>, List<BookViewModel>>(books);
            return PartialView(result);
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