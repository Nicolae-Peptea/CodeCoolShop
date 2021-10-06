using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class OrderService
    {
        private readonly IOrderDao order;

        public OrderService(IOrderDao order)
        {
            this.order = order;
        }

        public void BuyProduct(Item item)
        {
            order.Add(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return this.order.GetAll();
        }


        public string GetItemsAsJson()
        {
            IEnumerable<Item> orderItems = GetAllItems();
            string orderItemsAsJson = JsonConvert.SerializeObject(orderItems);

            return orderItemsAsJson;
        }
    }
}
