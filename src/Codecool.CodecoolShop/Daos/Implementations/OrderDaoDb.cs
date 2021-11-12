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

        //public void Add(OrderItem order)
        //{
        //    var itemInList = Get(order.Product.Id);

        //    if (itemInList != null)
        //    {
        //        itemInList.Quantity += 1;
        //    }
        //    else
        //    {
        //        data.Add(order);
        //    }
        //}

        //public OrderItem Get(int id)
        //{
        //    return data.Find(item => item.Product.Id == id);
        //}

        //public IEnumerable<OrderItem> GetAll()
        //{
        //    return data;
        //}

        //public void Delete(int id)
        //{
        //    if (this.Get(id) != null)
        //    {
        //        data.Remove(this.Get(id));
        //    }
        //}
    }
}
