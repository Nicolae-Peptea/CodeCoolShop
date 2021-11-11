using DataAccessLayer.Data;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDb : ISupplierDao
    {
        private readonly CodeCoolShopContext _context;

        private SupplierDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }

        public void Add(Supplier item)
        {
            item.Id = _context.Suppliers.Count() + 1;
            _context.Suppliers.Add(item);
            _context.SaveChangesAsync();
        }

        public void RemoveItem(int id)
        {
            _context.Suppliers.Remove(this.Get(id));
            _context.SaveChangesAsync();
        }

        public Supplier Get(int id)
        {
            var suppliers = GetAll().ToList();
            return suppliers.Find(supplier => supplier.Id == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;
        }
    }
}
