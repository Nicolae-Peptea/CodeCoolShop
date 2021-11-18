using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class ProductOrderServices : IProductOrderServices
    {
        private readonly IOrderDao _orderDao;
        private readonly IProductOrderDao _productOrderDao;

        public ProductOrderServices(IOrderDao order, IProductOrderDao productOrderDao)
        {
            _orderDao = order;
            _productOrderDao = productOrderDao;
        }

        public void AddProducts(List<ProductOrder> productOrders)
        {
            int latestAddedOrder = _orderDao.GetLatestAddedOrder().Id;

            foreach (var product in productOrders)
            {
                ProductOrder newProduct = new(product);
                newProduct.OrderId = latestAddedOrder;
                _productOrderDao.Add(newProduct);
            }
        }

        public List<ProductOrder> GetAllByOrder(int id)
        {
            return _productOrderDao.GetAllByOrder(id).ToList();
        }
    }
}
