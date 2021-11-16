using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public List<Order> All()
        {
            List<Order> orders = _orderServices.GetAllItems();
            return orders;
        }
    }
}
