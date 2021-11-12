using DataAccessLayer.Data;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductOrderDaoDb : IProductOrder
    {
        private readonly CodeCoolShopContext _context;

        public ProductOrderDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }
        public void Add(ProductOrder item)
        {
            throw new NotImplementedException();
        }

        public ProductOrder Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
