using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Supplier : BaseModel
    {
        public ICollection<Product> Products { get; set; }
    }
}
