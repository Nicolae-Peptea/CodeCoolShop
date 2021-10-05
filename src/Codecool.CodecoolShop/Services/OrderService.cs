using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
