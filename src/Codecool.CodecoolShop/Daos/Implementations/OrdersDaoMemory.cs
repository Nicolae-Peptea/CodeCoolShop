using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrdersDaoMemory : IOrdersDao
    {
        private List<OrderDetails> data = new List<OrderDetails>();
        private static OrdersDaoMemory instance = null;

        public static OrdersDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new OrdersDaoMemory();
            }

            return instance;
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
        }
    }
}
