using System.Collections.Generic;

namespace DataAccessLayer.Model
{
    public class Supplier : BaseModel
    {
        public ICollection<Product> Products { get; set; }
    }
}
