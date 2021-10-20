using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class EmailConfirmation
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Total { get; set; }

        public List<OrderItem> Items { get; set; }

        public EmailConfirmation(OrderDetails details, decimal orderTotal, List<OrderItem> orderItems)
        {
            this.FullName = details.StripeBillingName;
            this.Email = details.StripeEmail;
            this.Address = details.StripeBillingAddressLine1;
            this.Country = details.StripeBillingAddressCountry;
            this.City = details.StripeBillingAddressCity;
            this.ZipCode = details.StripeBillingAddressZip.ToString();
            this.Total = orderTotal.ToString();
            this.Items = orderItems;
        }
    }
}
