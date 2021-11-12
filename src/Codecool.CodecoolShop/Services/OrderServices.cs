using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using Stripe;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class OrderServices
    {
        private readonly IOrderDao _order;
        private readonly IProductOrder _productOrder;
        private readonly IProductDao _product;

        public OrderServices(IOrderDao order, IProductDao product, IProductOrder productOrder)
        {
            _order = order;
            _product = product;
            _productOrder = productOrder;

        }

        //public void Add(OrderItem item)
        //{
        //    this.order.Add(item);
        //}

        //public void RemoveItem(int productId)
        //{
        //    this.order.RemoveItem(productId);
        //}

        //public IEnumerable<OrderItem> GetAllItems()
        //{
        //    return this.order.GetAll();
        //}

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
