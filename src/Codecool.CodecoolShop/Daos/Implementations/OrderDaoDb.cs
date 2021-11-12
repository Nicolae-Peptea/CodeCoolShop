using DataAccessLayer.Data;
using DataAccessLayer.Model;
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

        public void Add(Order order)
        {
            //order.Id = _context.Orders.Count() + 1;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order Get(int id)
        {
            return _context.Orders.Where(product => product.Id == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        public void RemoveItem(int id)
        {
            _context.Orders.Remove(this.Get(id));
            _context.SaveChanges();
        }
    }
}
