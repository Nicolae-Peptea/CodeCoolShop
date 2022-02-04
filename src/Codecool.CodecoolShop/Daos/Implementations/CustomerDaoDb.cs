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

        public int GetCustomerIdByEmail(OrderViewDetailsModel order)
        {
            return _context.Customers
                .Where(customer => customer.Email == order.StripeEmail)
                .FirstOrDefault().Id;
        }

        public void CreateOrUpdateCustomer(Customer customer)
        {
            Customer alreadyCustomer = GetAlreadyCustomer(customer.Email);

            if (alreadyCustomer != null)
            {
                UpdateCustomer(customer, alreadyCustomer);
            }
            else
            {
                Add(customer);
            }
        }

        public Customer GetAlreadyCustomer(string email)
        {
            Customer customer = _context.Customers
                .Where(customer => customer.Email == email)
                .FirstOrDefault();
            return customer;
        }

        public void UpdateCustomer(Customer newCustomer, Customer existingCustomer)
        {
            string userId = existingCustomer.UserId ?? newCustomer.UserId;
            int customerId = existingCustomer.Id;

            if (existingCustomer.UserId != null)
            {
                _mapper.Map(newCustomer, existingCustomer);
            }
            else
            {
                _mapper.Map(existingCustomer, newCustomer);
            }

            existingCustomer.Id = customerId;
            existingCustomer.UserId = userId;

            _context.SaveChanges();
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

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
