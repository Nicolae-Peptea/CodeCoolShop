using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderServices orderServices,
            ICustomerService customerService,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderServices = orderServices;
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;
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

            List<Order> userOrders = _orderServices.GetByUserId(customerId);

            string json = JsonConvert.SerializeObject(userOrders, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            return json;
        }

        public string GetOrderProducts(int id)
        {
            //List<Order> orderProducts = _orderServices.GetOrderProducts(id);


            string json = JsonConvert.SerializeObject(id, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            return json;
        }
    }
}
