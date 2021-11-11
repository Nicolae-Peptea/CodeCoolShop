using DataAccessLayer.Model;
using DataAccessLayer.Data;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoDb : IProductCategoryDao
    {
        private readonly CodeCoolShopContext _context;
        public ProductCategoryDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }

        public void Add(Category item)
        {
            item.Id = _context.Categories.Count() + 1;
            _context.Categories.Add(item);
            _context.SaveChangesAsync();
        }

        public void RemoveItem(int id)
        {
            _context.Categories.Remove(this.Get(id));
            _context.SaveChangesAsync();
        }

        public Category Get(int id)
        {
            var categories = GetAll().ToList();
            return categories.Find(category => category.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            var x = _context.Categories;
            return _context.Categories;
        }
    }
}
