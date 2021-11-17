using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Customer
    {
        public int Id { get; set; }

#nullable enable

        public string? UserId { get; set; }

        [MaxLength(40)]
        public string? FirstName { get; set; }

        [MaxLength(40)]
        public string? LastName { get; set; }

        [MaxLength(40)]
        public string? BillingName { get; set; }

        [MaxLength(100)]
        public string? BillingAddressLine1 { get; set; }

        [MaxLength(25)]
        public long? BillingAddressZip { get; set; }

        [MaxLength(40)]
        public string? BillingAddressCity { get; set; }

        [MaxLength(40)]
        public string? BillingAddressCountry { get; set; }

        [MaxLength(25)]
        public long BillingAddressCountryCode { get; set; }

        [MaxLength(40)]
        public string? ShippingName { get; set; }

        [MaxLength(100)]
        public string? ShippingAddressLine1 { get; set; }

        [MaxLength(25)]
        public long? ShippingAddressZip { get; set; }

        [MaxLength(25)]
        public string? ShippingAddressCity { get; set; }

        [MaxLength(40)]
        public string? ShippingAddressCountry { get; set; }

        [MaxLength(25)]
        public long? ShippingAddressCountryCode { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(40)]
        public string? Email { get; set; }

#nullable disable

        public ICollection<Order> Orders { get; set; }
    }
}