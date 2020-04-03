using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Dynamic;
using WindowsOnly.Behaviors;
using WindowsOnly.DAL;
using WindowsOnly.Models;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Services
{
    public class AuthorService : IDisposable
    {
        private readonly BookInStockContext db = new BookInStockContext();

        public List<Author> Get(QueryOptions queryOptions)
        {
            int start = QueryOptionCalculator.CalculateStart(queryOptions);
            IQueryable<Author> authors = db.Authors.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize);
            queryOptions.TotalPages = QueryOptionCalculator.CalculateTotalPages(db.Authors.Count(), queryOptions.PageSize);
            return authors.ToList();
        }

        public Author GetById(long id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                throw new ObjectNotFoundException($"Unable to find author with id {id}.");
            }

            return author;
        }

        public Author GetByName(string name)
        {
            Author author = db.Authors.Where(a => a.FirstName + ' ' + a.LastName == name).SingleOrDefault();
            if (author == null)
            {
                throw new ObjectNotFoundException($"Unable to find author with name {name}.");
            }

            return author;
        }

        public void Insert(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public void Update(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Author author)
        {
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}