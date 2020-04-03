using System;
using System.Collections.Generic;
using System.Linq;
using WindowsOnly.DAL;
using WindowsOnly.Models.DataModels;

namespace WindowsOnly.Services
{
    public class ShoppingCartService : IDisposable
    {
        private readonly ShoppingCartContext _db = new ShoppingCartContext();

        public ShoppingCart GetBySessionId(string sessionId)
        {
            ShoppingCart shoppingCart = _db.ShoppingCarts.Include("CommodityItems").Where(c => c.SessionId == sessionId).SingleOrDefault();
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    SessionId = sessionId,
                    CommodityItems = new List<CommodityItem>()
                };
                _db.ShoppingCarts.Add(shoppingCart);
                _db.SaveChanges();
            }
            return shoppingCart;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}