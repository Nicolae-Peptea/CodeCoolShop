using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class OrdersServices
    {
        private readonly IOrdersDao order;

        public OrdersServices(IOrdersDao order)
        {
            this.order = order;
        }

        public void RemoveItem(int productId)
        {
            this.order.RemoveItem(productId);
        }

        public IEnumerable<OrderDetails> GetAllItems()
        {
            return this.order.GetAll();
        }

        public decimal CalculateOrderTotal(List<CartItem> cartItems, IEnumerable<ShopProduct> productList)
        {
            decimal orderTotal = 0;

            foreach (var cartItem in cartItems)
            {
                var price = productList
                    .Where(x => x.Id == cartItem.ProductId).
                    Select(x => x.DefaultPrice).First();

                orderTotal += price * cartItem.ProductQuantity;
            }
            return orderTotal * 100;
        }
    }
}
