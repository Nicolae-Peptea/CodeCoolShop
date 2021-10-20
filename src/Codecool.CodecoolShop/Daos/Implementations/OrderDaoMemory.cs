using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IOrderDao
    {
        private List<Order> data = new List<Order>();
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

        public void Add(Order item)
        {
            throw new NotImplementedException();
        }

        public void Update(Order item, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int productId)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            return data;
        }

        public int GetTotalQuantity()
        {
            return 0;
            //return data.Select(x => x.Quantity).Sum();
        }
    }
}
