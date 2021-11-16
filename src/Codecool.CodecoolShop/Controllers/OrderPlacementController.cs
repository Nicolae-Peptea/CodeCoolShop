using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderPlacementController : Controller
    {
        public IOrderServices OrderServices { get; private set; }
        public IMailService EmailService { get; private set; }
        public SendgridSettings SendgridSettings { get; private set; }

        public OrderPlacementController(IMailService mailService,
            IOrderServices orderServices, IOptions<SendgridSettings> sendgridSettings)
        {
            OrderServices = orderServices;
            EmailService = mailService;
            SendgridSettings = sendgridSettings.Value;
        }

        [HttpPost]
        public IActionResult Index(OrderDetails order)
        {
            List<DataAccessLayer.Model.ProductOrder> orderItems = OrderServices.UpdateProductOrderPriceFromJson(order);
            decimal orderTotal = OrderServices.GetTotalOrderValue(orderItems);

            OrderServices.CreateCustomer(order);
            //int customerId = 4;
            try
            {
                OrderServices.ChargeCustomer(order, orderTotal);
                OrderServices.CreateOrder(order, HttpContext);

                Log.Information("Successful checkout process - payment complete");
                EmailConfirmation model = new(order, orderTotal, orderItems);
                EmailService.SendEmail(model, SendgridSettings).Wait();
                //OrderServices.Add(customerId);
                return RedirectToAction("SuccessfulOrder", new { id = 1 });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed the checkout process due to payment");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SuccessfulOrder(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
