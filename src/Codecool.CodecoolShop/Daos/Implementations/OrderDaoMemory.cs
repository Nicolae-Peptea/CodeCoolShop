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
            var isItemInListAlready = data.Find(x => x.Product.Id == item.Product.Id);

            if (isItemInListAlready != null)
            {
                isItemInListAlready.Quantity++;
            }
            else
            {
                data.Add(item);
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Item Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
