using Codecool.CodecoolShop.Models;
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

        public async void Add(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Order Get(int id)
        {
            return _context.Orders.Where(product => product.Id == id)
                .FirstOrDefault();
        }

        public Order GetLatestAddedOrder()
        {
            return _context.Orders
                .OrderByDescending(order => order.OrderPlaced)
                .FirstOrDefault();
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
