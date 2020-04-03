using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using WindowsOnly.Models.DataModels;
using WindowsOnly.Models.ViewModels;
using WindowsOnly.Services;

namespace WindowsOnly.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryService _service = new CategoryService();
        private readonly MapperConfiguration _mapperConfig = new MapperConfiguration(config => 
        {
            config.CreateMap<Category, CategoryViewModel>();
            config.CreateMap<CategoryViewModel, Category>();
        });

        [ChildActionOnly]
        public PartialViewResult Menu(int selectedCategoryId)
        {
            List<Category> categories = _service.Get();
            ViewBag.SelectedCategoryId = selectedCategoryId;
            return PartialView(_mapperConfig.CreateMapper().Map<List<CategoryViewModel>>(categories));
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