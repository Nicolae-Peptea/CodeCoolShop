using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderDao _order;
        private readonly IProductOrder _productOrder;
        private readonly IProductDao _product;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderServices(IOrderDao order, IProductDao product,
            IProductOrder productOrder, UserManager<IdentityUser> userManager)
        {
            _order = order;
            _product = product;
            _productOrder = productOrder;
            _userManager = userManager;
        }

        public void Add(int customerId)
        {
            DataAccessLayer.Model.Order newOrder = new();
            newOrder.OrderPlaced = DateTime.Now;
            //newOrder.CustomerId = customerId;
            _order.Add(newOrder);
        }

        public DataAccessLayer.Model.Order Get(int id)
        {
            return _order.Get(id);
        }

        public void RemoveItem(int id)
        {
            _order.RemoveItem(id);
        }

        public IEnumerable<DataAccessLayer.Model.Order> GetAllItems()
        {
            return _order.GetAll();
        }

        public decimal GetTotalOrderValue(List<ProductOrder> orderItems)
        {
            return orderItems.Select(item => item.PricePerProduct * item.Quantity).Sum();
        }


        public List<ProductOrder> UpdateProductOrderPriceFromJson(OrderDetails order)
        {
            List<ProductOrder> orderItems = JsonHelper.Deserialize<List<ProductOrder>>(order.CartItems);

            foreach (var item in orderItems)
            {
                item.Product = _product.Get(item.ProductId);
                item.PricePerProduct = item.Product.Price;
            }

            return orderItems;
        }

        public void CreateCustomer(OrderDetails order)
        {
            CustomerService customers = new();

            customers.Create(new CustomerCreateOptions
            {
                Email = order.StripeEmail,
                Name = order.StripeBillingName,
            });
        }

        public void CreateOrder(OrderDetails order, HttpContext httpContext)
        {

            var x = _userManager.GetUserId(httpContext.User);
           
            DataAccessLayer.Model.Order dbOrder = new()
            {
                OrderPlaced = DateTime.Now,
            };

            if (x != null)
            {
                var y = 5;
            }
        }

        public void ChargeCustomer(OrderDetails order, decimal orderTotal)
        {
            ChargeService charges = new();

            charges.Create(new ChargeCreateOptions
            {
                Amount = (long)orderTotal * 100,
                Description = "Test Payment",
                Currency = "usd",
                Source = order.StripeToken,
            });
        }
    }
}
