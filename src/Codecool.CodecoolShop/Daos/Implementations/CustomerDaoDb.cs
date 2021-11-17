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
            _context.Customers.Add(item);
            _context.SaveChangesAsync();
        }

        public Customer Get(int id)
        {
            return _context.Customers
                .Where(customer => customer.Id == id)
                .First();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetId(string userId)
        {
            return _context.Customers
                .Where(customer => customer.UserId == userId)
                .First().Id;
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
