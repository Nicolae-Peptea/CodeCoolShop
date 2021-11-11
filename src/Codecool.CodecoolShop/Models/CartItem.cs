using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Models
{
    public class CartItem
    {
        [JsonProperty("productId")]
        public long ProductId { get; set; }

        [JsonProperty("productQuantity")]
        public int ProductQuantity { get; set; }
    }
}
