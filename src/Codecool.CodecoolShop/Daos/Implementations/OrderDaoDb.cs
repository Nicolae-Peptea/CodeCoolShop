using Codecool.CodecoolShop.Models;
using DataAccessLayer.Data;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoDb : IOrderDao
    {
        private readonly CodeCoolShopContext _context;

        public OrderDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }

        public void Add(OrderItem order)
        {
            var itemInList = Get(order.Product.Id);

            if (itemInList != null)
            {
                itemInList.Quantity += 1;
            }
            else
            {
                data.Add(order);
            }
        }

        public void RemoveItem(int id)
        {
            if (this.Get(id) != null)
            {
                data.Remove(this.Get(id));
            }
        }

        public OrderItem Get(int id)
        {
            return data.Find(item => item.Product.Id == id);
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return data;
        }

        public int GetTotalQuantity()
        {
            return data.Select(item => item.Quantity).Sum();
        }

        public decimal GetTotalValue()
        {
            return data.Select(item => item.Product.DefaultPrice * item.Quantity).Sum();
        }

        public void EmptyOrder()
        {
            data = new List<OrderItem>();
        }
    }
}
