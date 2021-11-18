namespace Codecool.CodecoolShop.Models
{
    public class SendgridSettings
    {
        public string SenderEmail { get; set; }
        public string ApiKey { get; set; }
        public string OrderConfirmationTemplateId { get; set; }
        public string AccountConfirmationTemplateId { get; set; }
    }
}
