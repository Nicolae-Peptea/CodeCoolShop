using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class ProductOrder
    {
        public int Id { get; set; }

        [JsonProperty("productQuantity")]
        public int Quantity { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerProduct { get; set; }

        public ProductOrder()
        {
        }

        public ProductOrder(ProductOrder oldProduct)
        {
            Quantity = oldProduct.Quantity;
            ProductId = oldProduct.ProductId;
            OrderId = oldProduct.OrderId;
            PricePerProduct = oldProduct.PricePerProduct;
        }
    }
}