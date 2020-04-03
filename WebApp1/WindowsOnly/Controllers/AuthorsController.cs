using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WindowsOnly.Filters;
using WindowsOnly.Models;
using WindowsOnly.Services;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Controllers
{
    public class AuthorsController : Controller
    {
        private AuthorService authorService;
        private MapperConfiguration mapperConfig;

        public AuthorsController()
        {
            authorService = new AuthorService();
            mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Author, AuthorViewModel>();
                config.CreateMap<AuthorViewModel, Author>();
            });

        }

        // GET: Authors
        [GenerateResultListFilter(typeof(Author), typeof(AuthorViewModel))]
        public ActionResult Index([Form] QueryOptions queryOptions)
        {
            List<Author> authors = authorService.Get(queryOptions);
            ViewData["QueryOptions"] = queryOptions;
            return View(authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = authorService.GetById(id.Value);
            IMapper mapper = mapperConfig.CreateMapper();
            AuthorViewModel authorViewModel = mapper.Map<AuthorViewModel>(author);
            return View("DetailsModal", authorViewModel);
        }

        // GET: Authors/Create
        [BasicAuthorizationFilter]
        public ActionResult Create()
        {
            return View("Form", new AuthorViewModel());
        }

        // POST: Authors/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                IMapper mapper = mapperConfig.CreateMapper();
                Author author = mapper.Map<Author>(authorViewModel);
                authorService.Insert(author);
                return RedirectToAction("Index");
            }
            else 
            {
                return View("Form", authorViewModel);
            }
        }

        // GET: Authors/Edit/5
        [BasicAuthorizationFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = authorService.GetById(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }

            IMapper mapper = mapperConfig.CreateMapper();
            AuthorViewModel authorViewModel = mapper.Map<AuthorViewModel>(author);
            return View("Form", authorViewModel);
        }

        // POST: Authors/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                Author author = mapperConfig.CreateMapper().Map<Author>(authorViewModel);
                authorService.Update(author);
                return RedirectToAction("Index");
            }
            return View("Form", authorViewModel);
        }

        // GET: Authors/Delete/5
        [BasicAuthorizationFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = authorService.GetById(id.Value);
            IMapper mapper = mapperConfig.CreateMapper();
            AuthorViewModel authorViewModel = mapper.Map<AuthorViewModel>(author);
            return View(authorViewModel);
        }

        // POST: Authors/Delete/5
        [BasicAuthorizationFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = authorService.GetById(id);
            authorService.Delete(author);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                authorService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
