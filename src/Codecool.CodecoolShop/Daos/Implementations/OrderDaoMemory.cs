using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IOrderDao
    {
        private List<Product> data = new List<Product>();
        private static OrderDaoMemory instance = null;


        public static OrderDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderDaoMemory();
            }

            return instance;
        }


        public void Add(Product item)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory, Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int GetTotalValue()
        {
            throw new NotImplementedException();
        }
    }
}
