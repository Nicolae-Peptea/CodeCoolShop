//using Codecool.CodecoolShop.Models;
//using DataAccessLayer.Data;
//using System;
//using System.Collections.Generic;

//namespace Codecool.CodecoolShop.Daos.Implementations
//{
//    public class OrdersDaoDb : IOrdersDao
//    {
//        private readonly CodeCoolShopContext _context;

//        public OrdersDaoDb(CodeCoolShopContext context)
//        {
//            _context = context;
//        }

//        public void Add(OrderDetails order)
//        {
//            data.Add(order);
//        }

//        public void Update(OrderDetails item, int quantity)
//        {
//            throw new NotImplementedException();
//        }

//        public void RemoveItem(int productId)
//        {
//            throw new NotImplementedException();
//        }

//        public OrderDetails Get(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<OrderDetails> GetAll()
//        {
//            return data;
//        }

//        public int GetTotalQuantity()
//        {
//            return 0;
//        }
//    }
//}
