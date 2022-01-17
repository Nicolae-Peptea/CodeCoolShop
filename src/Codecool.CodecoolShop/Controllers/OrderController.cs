using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly IProductOrderServices _productOrderServices;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderServices orderServices,
            ICustomerService customerService,
            IHttpContextAccessor httpContextAccessor,
            IProductOrderServices productOrderServices)
        {
            _orderServices = orderServices;
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;
            _productOrderServices = productOrderServices;
        }

        public List<Order> All()
        {
            List<Order> orders = _orderServices.GetAllItems();
            return orders;
        }

        public string GetByUserInSession()
        {
            ClaimsPrincipal userInSession = _httpContextAccessor.HttpContext.User;
            string userId = _customerService.GetUserId(userInSession);
            int customerId = _customerService.GetCustomerId(userId);

            List<Order> userOrders = _orderServices.GetOrderByUserId(customerId);

            string json = JsonConvert.SerializeObject(userOrders, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            return json;
        }

        public string GetOrderProducts(int id)
        {
            List<ProductOrder> orderProducts = _productOrderServices.GetAllByOrder(id);

            string json = JsonConvert.SerializeObject(orderProducts, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            return json;
        }
    }
}
