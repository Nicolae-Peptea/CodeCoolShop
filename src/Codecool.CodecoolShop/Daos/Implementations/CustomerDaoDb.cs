using DataAccessLayer.Data;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CustomerDaoDb : ICustomerDao
    {
        private readonly CodeCoolShopContext _context;

        public CustomerDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }

        public void Add(Customer item)
        {
            _context.Add(item);
            _context.SaveChangesAsync();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
