using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IOrderServices
    {
        void AddOrder(OrderViewDetails order);
        
        Order Get(int id);
        
        void RemoveItem(int id);
        
        IEnumerable<Order> GetAllItems();
        
        decimal GetTotalOrderValue(List<ProductOrder> orderItems);
        
        List<ProductOrder> UpdateProductOrderPriceFromJson(OrderViewDetails order);
        
        void ChargeCustomer(OrderViewDetails order, decimal orderTotal);
    }
}
