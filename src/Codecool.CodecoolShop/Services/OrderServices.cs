using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class OrderServices
    {
        private readonly IOrderDao order;

        public OrderServices(IOrderDao order)
        {
            this.order = order;
        }

        public void Add(OrderItem item)
        {
            this.order.Add(item);
        }

        public void RemoveItem(int productId)
        {
            this.order.RemoveItem(productId);
        }

        public IEnumerable<OrderItem> GetAllItems()
        {
            return this.order.GetAll();
        }

        public decimal GetTotalOrderValue()
        {
            return this.order.GetTotalValue();
        }

        public int GetTotalQuantity()
        {
            return this.order.GetTotalQuantity();
        }

        public IEnumerable<OrderItem> GetOrderItems(List<CartItem> cartItems, IEnumerable<ShopProduct> products)
        {
            foreach (ShopProduct product in products)
            {
                foreach (CartItem item in cartItems)
                {
                    if (product.Id == item.ProductId)
                    {
                        OrderItem orderItem = new OrderItem(product, item.ProductQuantity);
                        this.Add(orderItem);
                    }
                }
            }
            return this.order.GetAll();
        }
    }
}
