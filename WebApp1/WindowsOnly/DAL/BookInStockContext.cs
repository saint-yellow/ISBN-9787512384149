using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WindowsOnly.Models;

namespace WindowsOnly.DAL
{
    public class BookInStockContext : DbContext
    {
        public BookInStockContext() : base("BookInStockContext")
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}