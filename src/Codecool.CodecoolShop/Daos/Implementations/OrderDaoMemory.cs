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
            var itemInList = Get(item.Product.Id);

            if (itemInList != null)
            {
                itemInList.Quantity++;
            }
            else
            {
                data.Add(item);
            }
        }

        public void RemoveItem(int productId)
        {
            data.Remove(data.Single(item => item.Product.Id == productId));
        }


        public void RemoveItemQuantity(Item item)
        {
            var itemInList = Get(item.Product.Id);
            if (itemInList != null)
            {
                itemInList.Quantity--;
            }
            else
            {
                RemoveItem(item.Product.Id);
            }
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
