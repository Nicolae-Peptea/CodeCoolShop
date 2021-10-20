using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IOrderDao
    {
        private List<OrderDetails> data = new List<OrderDetails>();
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

        public void Add(OrderDetails order)
        {
            data.Add(order);
        }

        public void Update(OrderDetails item, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int productId)
        {
            throw new NotImplementedException();
        }

        public OrderDetails Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetails> GetAll()
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
