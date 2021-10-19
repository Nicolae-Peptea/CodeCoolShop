﻿using Codecool.CodecoolShop.Models;
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

        public decimal GetTotalValue()
        {
            decimal total = 0;
            foreach (Item item in data)
            {
                total += item.Product.DefaultPrice * item.Quantity;
            }
            return total;
        }

        public void Add(Item item)
        {
            throw new NotImplementedException();
        }

        public void Update(Item item, int quantity)
        {
            var itemInList = Get(item.Product.Id);

            if (itemInList != null)
            {
                itemInList.Quantity += quantity;
            }
            else
            {
                data.Add(item);
            }

            if (itemInList != null && itemInList.Quantity == 0)
            {
                RemoveItem(item.Product.Id);
            }
        }

        public void RemoveItem(int productId)
        {
            if (data.Any(item => item.Product.Id == productId))
            {
                data.Remove(data.Single(item => item.Product.Id == productId));
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
