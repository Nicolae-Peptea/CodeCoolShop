using DataAccessLayer.Data;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductOrderDaoDb : IProductOrderDao
    {
        private readonly CodeCoolShopContext _context;

        public ProductOrderDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }
        public void Add(ProductOrder item)
        {
             _context.ProductOrders.Add(item);
             _context.SaveChanges();
        }

        public ProductOrder Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductOrder> GetAllByOrder(int id)
        {
            return _context.ProductOrders
                .Where(item => item.Order.Id == id)
                .Include(item => item.Product);
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
