using DataAccessLayer.Data;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDb : IProductDao
    {
        private readonly CodeCoolShopContext _context;

        public ProductDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }

        public void Add(Product item)
        {
            item.Id = _context.Products.Count() + 1;
            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public void RemoveItem(int id)
        {
            _context.Products.Remove(this.Get(id));
            _context.SaveChanges();
        }

        public Product Get(int id)
        {
            return _context.Products.Where(product => product.Id == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return _context.Products.Where(x => x.Supplier.Id == supplier.Id);
        }

        public IEnumerable<Product> GetBy(Category productCategory)
        {
            return _context.Products.Where(x => x.Category.Id == productCategory.Id);
        }

        public IEnumerable<Product> GetBy(Category productCategory,
            Supplier supplier)
        {
            return _context.Products.Where(x => x.Category.Id == productCategory.Id)
                .Where(x => x.Supplier.Id == supplier.Id);
        }
    }
}
