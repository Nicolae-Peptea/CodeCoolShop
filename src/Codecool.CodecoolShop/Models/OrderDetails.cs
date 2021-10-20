using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class OrderDetails : BaseModel
    {
        public string StripeEmail { get; set; }
        public string StripeToken { get; set; }
        public string CartItems { get; set; }
        public string StripeBillingName { get; set; }
        public string StripeBillingAddressLine1 { get; set; }
        public long StripeBillingAddressZip { get; set; }
        public string StripeBillingAddressCity { get; set; }
        public string StripeBillingAddressCountry { get; set; }
        public long StripeBillingAddressCountryCode { get; set; }
        public string StripeShippingName { get; set; }
        public string StripeShippingAddressLine1 { get; set; }
        public long StripeShippingAddressZip { get; set; }
        public string StripeShippingAddressCity { get; set; }
        public string StripeShippingAddressCountry { get; set; }
        public long StripeShippingAddressCountryCode { get; set; }
    }
}
