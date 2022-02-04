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

        public CustomerServices(UserManager<IdentityUser> userManager, ICustomerDao customerDao,
            IMapper mapper)
        {
            _userManager = userManager;
            _customerDao = customerDao;
            _mapper = mapper;
        }

        public void CreateCustomerFromOrder(OrderViewDetailsModel order, HttpContext httpContext)
        {
            CustomerService stripeCustomer = new();

            stripeCustomer.Create(new CustomerCreateOptions
            {
                Email = order.StripeEmail,
                Name = order.StripeBillingName,
            });

            DataAccessLayer.Model.Customer newCustomer = MapOrderDetailsToCustomerModel(order);

            _customerDao.CreateOrUpdateCustomer(newCustomer);
        }

        public void CreateOrUpdateCustomerOnEmailConfirmation (string email, string userId)
        {
            DataAccessLayer.Model.Customer newCustomer = new()
            {
                UserId = userId,
                Email = email,
            };

            _customerDao.CreateOrUpdateCustomer(newCustomer);
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
