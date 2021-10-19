using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IOrderDao
    {
        private List<CartItem> data = new List<CartItem>();
        private static OrderDaoMemory instance = null;

        public static OrderDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderDaoMemory();
            }

            return instance;
        }

        public int GetTotalValue()
        {
            throw new NotImplementedException();
        }

        public void Add(CartItem item)
        {
            throw new NotImplementedException();
        }

        public void Update(CartItem item, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int productId)
        {
            throw new NotImplementedException();
        }

        public CartItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItem> GetAll()
        {
            return data;
        }

        public int GetTotalQuantity()
        {
            return data.Select(x => x.Quantity).Sum();
        }
    }
}
