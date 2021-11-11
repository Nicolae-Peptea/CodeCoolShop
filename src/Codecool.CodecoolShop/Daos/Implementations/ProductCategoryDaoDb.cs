using Codecool.CodecoolShop.Models;
using DataAccessLayer.Data;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoDb : IProductCategoryDao
    {
        private readonly CodeCoolShopContext _context;
        public ProductCategoryDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }

        public void Add(ProductCategory item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void RemoveItem(int id)
        {
            data.Remove(this.Get(id));
        }

        public ProductCategory Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return data;
        }
    }
}
