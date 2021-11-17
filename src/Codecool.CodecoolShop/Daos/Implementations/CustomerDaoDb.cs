using Codecool.CodecoolShop.Models;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
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
            throw new NotImplementedException();
        }

        public bool IsAlreadyCustomer(OrderDetails order)
        {
            Customer customer =_context.Customers
                .Where(customer => customer.Email == order.StripeEmail)
                .FirstOrDefault();
            return customer == null;
        }

        public void UpdateCustomer(Customer customer)
        {
            Customer existingCustomer = _context.Customers
                .Where(c => c.Email == customer.Email)
                .FirstOrDefault();

            existingCustomer.BillingName = customer.BillingName;
            existingCustomer.BillingAddressCity = customer.BillingAddressCity;
            existingCustomer.BillingAddressCountry = customer.BillingAddressCountry;
            existingCustomer.BillingAddressCountryCode = customer.BillingAddressCountryCode;
            existingCustomer.BillingAddressLine1 = customer.BillingAddressLine1;
            existingCustomer.BillingAddressZip = customer.BillingAddressZip;
            existingCustomer.Email = customer.Email;
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;

            existingCustomer.ShippingName = customer.ShippingName;
            existingCustomer.ShippingAddressCity = customer.ShippingAddressCity;
            existingCustomer.ShippingAddressCountry = customer.ShippingAddressCountry;
            existingCustomer.ShippingAddressCountryCode = customer.ShippingAddressCountryCode;
            existingCustomer.ShippingAddressLine1 = customer.ShippingAddressLine1;
            existingCustomer.ShippingAddressZip = customer.ShippingAddressZip;
            existingCustomer.UserId = customer.UserId;

            _context.SaveChangesAsync();
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
