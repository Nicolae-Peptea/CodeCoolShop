namespace Codecool.CodecoolShop.Models
{
    public class OrderItem
    {
        public ShopProduct Product { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public OrderItem(ShopProduct product, int productQuantity)
        {
            this.Product = product;
            this.ProductId = product.Id;
            this.ProductName = product.Name;
            this.ProductPrice = product.DefaultPrice;
            this.Quantity = productQuantity;
        }
    }
}
