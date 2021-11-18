using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderPlacementController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IMailService _emailService;
        private readonly ICustomerService _customerService;
        private readonly IProductOrderServices _productOrderService;

        public OrderPlacementController(IMailService mailService,
            IOrderServices orderServices,
            ICustomerService customerService, IProductOrderServices productOrderServices)
        {
            _orderServices = orderServices;
            _emailService = mailService;
            _customerService = customerService;
            _productOrderService = productOrderServices;
        }

        [HttpPost]
        public IActionResult Index(OrderViewDetailsModel order)
        {
            List<DataAccessLayer.Model.ProductOrder> orderItems = _orderServices.UpdateProductOrderPriceFromJson(order);
            decimal orderTotal = _orderServices.GetTotalOrderValue(orderItems);

            try
            {
                _customerService.CreateCustomer(order, HttpContext);
                _orderServices.AddOrder(order);

                _orderServices.ChargeCustomer(order, orderTotal);
                _productOrderService.AddProducts(orderItems);

                Log.Information("Successful checkout process - payment complete");
                SendgridOrderConfirmationModel model = new(order, orderTotal, orderItems);
                _emailService.SendOrderConfirmation(model).Wait();
                return RedirectToAction("SuccessfulOrder", new { id = _orderServices.GetLatestOrderId() });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed the checkout process due to payment");
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
