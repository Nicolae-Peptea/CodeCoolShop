using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using Stripe;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderPlacementController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IMailService _emailService;
        private readonly ICustomerService _customerService;
        private readonly IProductOrderServices _productOrderService;
        private readonly IConfiguration _configuration;

        public OrderPlacementController(IMailService mailService, IOrderServices orderServices,
           ICustomerService customerService, IProductOrderServices productOrderServices,
           IConfiguration confirguration)
        {
            _orderServices = orderServices;
            _emailService = mailService;
            _customerService = customerService;
            _productOrderService = productOrderServices;
            _configuration = confirguration;
        }

        [HttpPost]
        public IActionResult Index(OrderViewDetailsModel order)
        {
            List<DataAccessLayer.Model.ProductOrder> orderItems = _orderServices.UpdateProductOrderPriceFromJson(order);
            decimal orderTotal = _orderServices.GetTotalOrderValue(orderItems);

            try
            {
                _orderServices.ChargeCustomer(order, orderTotal);

                _customerService.CreateCustomer(order, HttpContext);
                _orderServices.AddOrder(order);
                _productOrderService.AddProducts(orderItems);

                string sendgridTemplateId = _configuration.GetValue<string>("Sendgrid:OrderConfirmationTemplateId");
                SendgridOrderConfirmationModel emailModel = new(order, orderTotal, orderItems, sendgridTemplateId);
                _emailService.SendEmail(emailModel).Wait();

                return RedirectToAction("SuccessfulOrder", new { id = _orderServices.GetLatestOrderId() });
            }
            catch (StripeException ex)
            {
                Log.Error(ex, "Failed to process the payment");
            }

            return RedirectToAction("Index", "HomePage");
        }

        public IActionResult SuccessfulOrder(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
