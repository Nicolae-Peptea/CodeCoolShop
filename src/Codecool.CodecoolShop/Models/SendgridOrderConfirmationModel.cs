using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class SendgridOrderConfirmationModel : SendgridBaseModel
    {
        public string FullName { get; private set; }

        public string Address { get; private set; }

        public string Country { get; private set; }

        public string City { get; private set; }

        public string ZipCode { get; private set; }

        public string Total { get; private set; }

        public List<ProductOrder> Items { get; private set; }

        public SendgridOrderConfirmationModel(OrderViewDetails details, decimal orderTotal, List<ProductOrder> orderItems)
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
