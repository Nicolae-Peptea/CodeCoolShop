using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Customer
    {
        public int Id { get; set; }

#nullable enable

        public string UserId { get; set; }

        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string? ShippingAddress { get; set; }

        [MaxLength(100)]
        public string? BillingAddress { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(40)]
        public string? Email { get; set; }

#nullable disable

        public ICollection<Order> Orders { get; set; }
    }
}