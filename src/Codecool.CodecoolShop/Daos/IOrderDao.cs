﻿using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<Order>
    {
        IEnumerable<Order> GetByCustomerId(int customerId);
    }
}
