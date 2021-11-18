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
        public IOrderServices OrderServices { get; private set; }
        public IMailService EmailService { get; private set; }
        public ICustomerService CustomerService { get; private set; }
        public IProductOrderServices ProductOrderService { get; private set; }

        public OrderPlacementController(IMailService mailService,
            IOrderServices orderServices,
            ICustomerService customerService, IProductOrderServices productOrderServices)
        {
            OrderServices = orderServices;
            EmailService = mailService;
            CustomerService = customerService;
            ProductOrderService = productOrderServices;
        }

        [HttpPost]
        public IActionResult Index(OrderViewDetails order)
        {
            List<DataAccessLayer.Model.ProductOrder> orderItems = OrderServices.UpdateProductOrderPriceFromJson(order);
            decimal orderTotal = OrderServices.GetTotalOrderValue(orderItems);

            try
            {
                CustomerService.CreateCustomer(order, HttpContext);
                OrderServices.AddOrder(order);

                OrderServices.ChargeCustomer(order, orderTotal);
                ProductOrderService.AddProducts(orderItems);

                Log.Information("Successful checkout process - payment complete");
                SendgridOrderConfirmationModel model = new(order, orderTotal, orderItems);
                EmailService.SendOrderConfirmation(model).Wait();
                return RedirectToAction("SuccessfulOrder", new { id = OrderServices.GetLatestOrderId() });
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
