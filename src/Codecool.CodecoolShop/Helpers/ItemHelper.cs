using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Helpers
{
    public static class ItemHelper
    {
        public static Item GetItem( Product product)
        {
            Item item = new Item();
            Product boughtProduct = product;
            item.Product = boughtProduct;

            return item;
        }
    }
}
