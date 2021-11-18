using Codecool.CodecoolShop.Models;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _context.SaveChanges();
        }

        public Customer Get(int id)
        {
            return _context.Customers
                .Where(customer => customer.Id == id)
                .First();
        }

        public bool IsAlreadyCustomer(OrderViewDetailsModel order)
        {
            Customer customer = _context.Customers
                .Where(customer => customer.Email == order.StripeEmail)
                .FirstOrDefault();
            return customer == null;
        }

        public int GetCustomerIdByEmail(OrderViewDetailsModel order)
        {
            return _context.Customers
                .Where(customer => customer.Email == order.StripeEmail)
                .FirstOrDefault().Id;
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
