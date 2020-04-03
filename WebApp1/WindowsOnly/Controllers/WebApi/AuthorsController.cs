using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WindowsOnly.Models;
using WindowsOnly.Services;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Controllers.WebApi
{
    public class AuthorsController : ApiController
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

        [ResponseType(typeof(AuthorViewModel))]
        public ResultList<AuthorViewModel> Get([FromUri] QueryOptions queryOptions)
        {
            List<Author> authors = authorService.Get(queryOptions);
            IMapper mapper = mapperConfig.CreateMapper();
            List<AuthorViewModel> results = mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList());
            ResultList<AuthorViewModel> resultList = new ResultList<AuthorViewModel>(results, queryOptions);
            return resultList;
        }

        public IHttpActionResult Get(int id)
        {
            Author author = authorService.GetById(id);
            IMapper mapper = mapperConfig.CreateMapper();
            AuthorViewModel authorViewModel = mapper.Map<AuthorViewModel>(author);
            return Ok(authorViewModel);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(AuthorViewModel authorViewModel)
        {
            IMapper mapper = mapperConfig.CreateMapper();
            Author author = mapper.Map<Author>(authorViewModel);
            authorService.Update(author);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(AuthorViewModel))]
        public IHttpActionResult Post(AuthorViewModel authorViewModel)
        {
            IMapper mapper = mapperConfig.CreateMapper();
            Author author = mapper.Map<Author>(authorViewModel);

            authorService.Insert(author);

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        [ResponseType(typeof(Author))]
        public IHttpActionResult Delete(int id)
        {
            Author author = authorService.GetById(id);
            authorService.Delete(author);
            return Ok(author);
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
