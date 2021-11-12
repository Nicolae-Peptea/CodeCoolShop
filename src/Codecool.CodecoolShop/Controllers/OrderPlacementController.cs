using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Stripe;
using System;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderPlacementController : Controller
    {
        public ProductServicesDb ProductService { get; private set; }
        public OrderServices OrderServices { get; private set; }
        public IMailService EmailService { get; private set; }

        public OrderPlacementController(IMailService mailService, CodeCoolShopContext context)
        {
            ProductDaoDb productDao = new(context);
            ProductOrderDaoDb productOrderDao = new(context);
            OrderDaoDb orderDao = new(context); 

            OrderServices = new OrderServices(orderDao, productDao, productOrderDao);
            EmailService = mailService;
        }

        [HttpPost]
        public IActionResult Index(OrderDetails order)
        {
            List<DataAccessLayer.Model.ProductOrder> orderItems = OrderServices.UpdateProductOrderPriceFromJson(order);
            decimal orderTotal = OrderServices.GetTotalOrderValue(orderItems);
            
            OrderServices.CreateCustomer(order);

            try
            {
                OrderServices.ChargeCustomer(order, orderTotal);

                Log.Information("Successful checkout process - payment complete");
                EmailConfirmation model = new(order, orderTotal, orderItems);
                EmailService.Execute(model).Wait();
                //aci o sa se salveze in db
                return RedirectToAction("SuccessfulOrder", new { id = 1 });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed the checkout process due to payment");
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult SuccessfulOrder(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
