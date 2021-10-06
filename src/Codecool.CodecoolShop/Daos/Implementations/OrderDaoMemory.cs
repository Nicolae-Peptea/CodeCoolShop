using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IOrderDao
    {
        private List<Item> data = new List<Item>();
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

        public void Add(Item item)
        {
            var isItemInListAlready = Get(item.Product.Id);

            if (isItemInListAlready != null)
            {
                isItemInListAlready.Quantity++;
            }
            else
            {
                data.Add(item);
            }
        }

        public void Remove(int productId)
        {
            data.Remove(data.Single(item => item.Product.Id == productId));
        }

        public Item Get(int id)
        {
            return data.Find(x => x.Product.Id == id);
        }

        public IEnumerable<Item> GetAll()
        {
            return data;
        }

        public int GetTotalQuantity()
        {
            return data.Select(x => x.Quantity).Sum();
        }
    }
}
