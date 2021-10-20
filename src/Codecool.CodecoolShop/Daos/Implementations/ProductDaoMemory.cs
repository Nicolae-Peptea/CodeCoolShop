using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoMemory : IProductDao
    {
        private List<ShopProduct> data = new List<ShopProduct>();
        private static ProductDaoMemory instance = null;

        public ProductDaoMemory()
        {
        }

        public static ProductDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductDaoMemory();
            }

            return instance;
        }

        public void Add(ShopProduct item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void RemoveItem(int id)
        {
            data.Remove(this.Get(id));
        }

        public ShopProduct Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<ShopProduct> GetAll()
        {
            return data;
        }

        public IEnumerable<ShopProduct> GetBy(Supplier supplier)
        {
            return data.Where(x => x.Supplier.Id == supplier.Id);
        }

        public IEnumerable<ShopProduct> GetBy(ProductCategory productCategory)
        {
            return data.Where(x => x.ProductCategory.Id == productCategory.Id);
        }

        public IEnumerable<ShopProduct> GetBy(ProductCategory productCategory, 
            Supplier supplier)
        {
            return data.Where(x => x.ProductCategory.Id == productCategory.Id)
                .Where(x => x.Supplier.Id == supplier.Id);
        }
    }
}
