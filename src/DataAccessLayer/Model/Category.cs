using System.Collections.Generic;

namespace DataAccessLayer.Model
{
    public class Category : BaseModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
