using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Supplier : BaseModel
    {
        public List<ShopProduct> Products { get; set; }
    }
}
