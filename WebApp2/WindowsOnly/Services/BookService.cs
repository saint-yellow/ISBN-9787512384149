using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using WindowsOnly.Models.DataModels;
using WindowsOnly.DAL;

namespace WindowsOnly.Services
{
    public class BookService : IDisposable
    {
        private readonly ShoppingCartContext _db = new ShoppingCartContext();

        public List<Book> GetByCategoryId(int categoryId)
        {
            List<Book> books = _db.Books.Where(b => b.CategoryId == categoryId).OrderByDescending(b => b.Featured).ToList();
            return books;
        }

        public List<Book> GetFeatured()
        {
            List<Book> books = _db.Books.Where(b => b.Featured).ToList();
            return books;
        }

        public Book GetById(int id)
        {
            Book book = _db.Books.Include("Author").Where(b => b.Id == id).SingleOrDefault();

            if (book == null)
            {
                throw new ObjectNotFoundException($"Unable to find book with {id}.");
            }
            else
            {
                return book;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}