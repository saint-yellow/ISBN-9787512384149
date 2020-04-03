using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WindowsOnly.DAL;
using WindowsOnly.Models.DataModels;

namespace WindowsOnly.Services
{
    public class CommodityItemService : IDisposable
    {
        private ShoppingCartContext _db = new ShoppingCartContext();

        public CommodityItem GetByShoppingCartIdAndBookId(int shoppingCartId, int bookId)
        {
            CommodityItem commodityItem = _db.CommodityItems.SingleOrDefault(c => c.ShoppingCartId == shoppingCartId && c.BookId == bookId);
            return commodityItem;
        }

        public CommodityItem AddToShoppingCart(CommodityItem item)
        {
            CommodityItem existingItem = GetByShoppingCartIdAndBookId(item.ShoppingCartId, item.BookId);
            if (existingItem == null)
            {
                _db.Entry(item).State = EntityState.Added;
                existingItem = item;
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }
            _db.SaveChanges();
            return existingItem;
        }

        public void UpdateInShoppingCart(CommodityItem item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteFromShoppingCart(CommodityItem item)
        {
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}