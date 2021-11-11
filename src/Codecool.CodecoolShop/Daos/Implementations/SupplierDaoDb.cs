using Codecool.CodecoolShop.Models;
using DataAccessLayer.Data;
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
            data.Add(item);
        }

        public void RemoveItem(int id)
        {
            data.Remove(this.Get(id));
        }

        public Supplier Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return data;
        }
    }
}
