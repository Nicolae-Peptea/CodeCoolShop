using AutoMapper;
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
        private readonly IMapper _mapper;

        public CustomerDaoDb(CodeCoolShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public Customer GetAlreadyCustomer(string email)
        {
            Customer customer = _context.Customers
                .Where(customer => customer.Email == email)
                .FirstOrDefault();
            return customer;
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

            _mapper.Map(customer, existingCustomer);
            var x = customer;
            var y = existingCustomer;

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
