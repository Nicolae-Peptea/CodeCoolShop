using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IOrderServices
    {
        void Add(int customerId);
        Order Get(int id);
        void RemoveItem(int id);
        List<Order> GetAllItems();
        decimal GetTotalOrderValue(List<ProductOrder> orderItems);
        List<ProductOrder> UpdateProductOrderPriceFromJson(OrderDetails order);
        void CreateCustomer(OrderDetails order);
        void ChargeCustomer(OrderDetails order, decimal orderTotal);
    }
}
