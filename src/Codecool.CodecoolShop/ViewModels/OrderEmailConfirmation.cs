using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class OrderEmailConfirmation
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Total { get; set; }

        public List<ProductOrder> Items { get; set; }

        public OrderEmailConfirmation(OrderViewDetails details, decimal orderTotal, List<ProductOrder> orderItems)
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
