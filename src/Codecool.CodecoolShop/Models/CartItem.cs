using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class CartItem
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }


        [JsonProperty("productQunatity")]
        public int Quantity { get; set; }
    }
}
