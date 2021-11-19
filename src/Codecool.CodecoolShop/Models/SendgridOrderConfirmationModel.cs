using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class SendgridOrderConfirmationModel : SendgridBaseModel
    {
        public string Address { get; private set; }

        public string Country { get; private set; }

        public string City { get; private set; }

        public string ZipCode { get; private set; }

        public string Total { get; private set; }

        public List<ProductOrder> Items { get; private set; }

        public SendgridOrderConfirmationModel(OrderViewDetailsModel details, decimal orderTotal,
            List<ProductOrder> orderItems, string templateId)
        {
            FullName = details.StripeBillingName;
            Email = details.StripeEmail;
            Address = details.StripeBillingAddressLine1;
            Country = details.StripeBillingAddressCountry;
            City = details.StripeBillingAddressCity;
            ZipCode = details.StripeBillingAddressZip.ToString();
            Total = orderTotal.ToString();
            Items = orderItems;
            TemplateId = templateId;

        }
    }
}
