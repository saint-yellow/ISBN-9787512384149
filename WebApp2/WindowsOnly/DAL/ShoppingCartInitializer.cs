using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WindowsOnly.Models.DataModels;

namespace WindowsOnly.DAL
{
    public class ShoppingCartInitializer : DropCreateDatabaseIfModelChanges<ShoppingCartContext>
    {
        protected override void Seed(ShoppingCartContext context)
        {
            List<Category> categories = new List<Category>
            {
                new Category("Technology"),
                new Category("Science Fiction"),
                new Category("Non Fiction"),
                new Category("Graphic Novels"),
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var author = new Author
            {
                Biography = "...",
                FirstName = "Jamie",
                LastName = "Munro"
            };

            var books = new List<Book>
            {
                new Book 
                {
                    Author = author,
                    Category = categories[0],
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                    Isbn = "1491914319",
                    Synopsis = "...",
                    Title = "Knockout.js: Building Dynamic Client-Side Web Applications"
                },
                new Book 
                {
                    Author = author,
                    Category = categories[0],
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51AkFkNeUxL._AA160_.jpg",
                    Isbn = "1449319548",
                    Synopsis = "...",
                    Title = "20 Recipes for Programming PhoneGap: Cross-Platform Mobile Development"
                },
                new Book 
                {
                    Author = author,
                    Category = categories[0],
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51LpqnDq8-L._AA160_.jpg",
                    Isbn = "1449309860",
                    Synopsis = "...",
                    Title = "20 Recipes for Programming MVC 3: Faster, Smarter Web Development"
                },
                new Book 
                {
                    Author = author,
                    Category = categories[0],
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/41JC54HEroL._AA160_.jpg",
                    Isbn = "1460954394",
                    Synopsis = "...",
                    Title = "Rapid Application Development with CakePHP"
                }
            };

            books.ForEach(b => context.Books.Add(b));

            context.SaveChanges();
        }

        public void SeedData(ShoppingCartContext context)
        {
            this.Seed(context);
        }
    }
}