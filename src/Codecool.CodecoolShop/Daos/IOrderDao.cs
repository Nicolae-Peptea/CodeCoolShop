﻿using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao //: IDao<Order>
    {
        decimal GetTotalValue(List<OrderItem> orderItems);
        //int GetTotalQuantity();
        //void EmptyOrder();
    }
}
