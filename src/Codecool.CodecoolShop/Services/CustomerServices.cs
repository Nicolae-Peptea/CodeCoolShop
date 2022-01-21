using AutoMapper;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Security.Claims;

namespace Codecool.CodecoolShop.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerDao _customerDao;
        private readonly IMapper _mapper;
        private readonly IUsersDao _usersDao;

        public CustomerServices(UserManager<IdentityUser> userManager, ICustomerDao customerDao,
            IMapper mapper, IUsersDao usersDao)
        {
            _userManager = userManager;
            _customerDao = customerDao;
            _mapper = mapper;
            _usersDao = usersDao;
        }

        public void CreateCustomer(OrderViewDetailsModel order, HttpContext httpContext)
        {
            CustomerService stripeCustomer = new();

            stripeCustomer.Create(new CustomerCreateOptions
            {
                Email = order.StripeEmail,
                Name = order.StripeBillingName,
            });

            DataAccessLayer.Model.Customer newCustomer = MapOrderDetailsToCustomerModel(order);
            
            DataAccessLayer.Model.Customer alreadyCustomer = 
                _customerDao.GetAlreadyCustomer(order.StripeEmail);
           


            if (alreadyCustomer != null)
            {
                //newCustomer.UserId = alreadyCustomer.UserId;
                _customerDao.UpdateCustomer(newCustomer);
            }
            else
            {
                _customerDao.Add(newCustomer);
            }
        }

        public void UpdateOrCreateCustomer (string email, string userId)
        {
            DataAccessLayer.Model.Customer alreadyCustomer = _customerDao.GetAlreadyCustomer(email);
            
            if (alreadyCustomer != null)
            {
                alreadyCustomer.UserId = userId;
                _customerDao.UpdateCustomer(alreadyCustomer);
            }
            else
            {
                DataAccessLayer.Model.Customer customer = new()
                {
                    UserId = userId,
                    Email = email,
                };

                _customerDao.Add(customer);
            }
        }

        private DataAccessLayer.Model.Customer MapOrderDetailsToCustomerModel(OrderViewDetailsModel order)
        {
            var customer = new DataAccessLayer.Model.Customer();
            _mapper.Map(order, customer);
            return customer;
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            var user = _userManager.GetUserId(principal);
            return user;
        }

        public int GetCustomerId(string userId)
        {
            int customerId = _customerDao.GetId(userId);
            return customerId;
        }

        public DataAccessLayer.Model.Customer Get(int id)
        {
            var customer = _customerDao.Get(id);
            return customer;
        }
    }
}
