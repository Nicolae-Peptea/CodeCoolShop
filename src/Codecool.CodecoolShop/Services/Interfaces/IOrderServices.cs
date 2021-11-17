using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IOrderServices
    {
        void Add(int customerId);
        
        Order Get(int id);
        
        void RemoveItem(int id);
        List<Order> GetAllItems();
        List<Order> GetByUserId(int customerId);
        decimal GetTotalOrderValue(List<ProductOrder> orderItems);
        
        List<ProductOrder> UpdateProductOrderPriceFromJson(OrderDetails order);
        
        void ChargeCustomer(OrderDetails order, decimal orderTotal);

        void CreateOrder(OrderDetails order, HttpContext httpContext);
    }
}
