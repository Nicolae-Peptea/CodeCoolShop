using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IOrderServices
    {
        void AddOrder(OrderViewDetailsModel order);

        Order Get(int id);

        void RemoveItem(int id);
        List<Order> GetAllItems();
        List<Order> GetOrderByUserId(int customerId);
        decimal GetTotalOrderValue(List<ProductOrder> orderItems);
        int GetLatestOrderId();
        List<ProductOrder> UpdateProductOrderPriceFromJson(OrderViewDetailsModel order);

        void ChargeCustomer(OrderViewDetailsModel order, decimal orderTotal);
    }
}
