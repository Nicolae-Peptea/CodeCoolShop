using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Models
{
    public class CartItem
    {
        [JsonProperty ("productId")]
        public long ProductId { get; set; }

        [JsonProperty("productQuantity")]
        public int ProductQuantity { get; set; }
    }
}
