using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WindowsOnly.Models.DataModels;

namespace WindowsOnly.DAL
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext() : base("ShoppingCartContext")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<CommodityItem> CommodityItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}