namespace Codecool.CodecoolShop.Models
{
    public abstract class SendgridBaseModel
    {
        public string Email { get; protected set; }

        public string FullName { get; protected set; }

        public string TemplateId { get; set; }
    }
}
