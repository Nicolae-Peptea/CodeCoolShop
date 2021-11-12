using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class OrderServices
    {
        private readonly IOrderDao _order;
        private readonly IProductOrder _productOrder;
        private readonly IProductDao _product;

        public OrderServices(IOrderDao order)
        {
            this._order = order;
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

        public decimal GetTotalOrderValue(List<OrderItem> orderItems)
        {
            return this._order.GetTotalValue(orderItems);
        }

        //public int GetTotalQuantity()
        //{
        //    return this.order.GetTotalQuantity();
        //}

        public List<OrderItem> GetOrderItems(List<ProductOrder> cartItems,
            IEnumerable<Product> products)
        {
            List<OrderItem> orderItems = new();

            foreach (Product product in products)
            {
                foreach (ProductOrder item in cartItems)
                {
                    if (product.Id == item.ProductId)
                    {
                        OrderItem orderItem = new(product, item.Quantity);
                        orderItems.Add(orderItem);
                    }
                }
            }
            return orderItems;
        }

        //public void EmptyOrder()
        //{
        //    this.order.EmptyOrder();
        //}
    }
}
