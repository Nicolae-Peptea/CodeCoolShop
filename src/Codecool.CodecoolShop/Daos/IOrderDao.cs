using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    interface IOrderDao : IProductDao
    {
        int GetTotalValue(); 
    }
}
