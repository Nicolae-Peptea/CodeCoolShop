using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class MailServices : IMailService
    {
        private readonly SendgridSettings _sendgridSettings;

        public MailServices(IOptions<SendgridSettings> sendgridSettings)
        {
            _sendgridSettings = sendgridSettings.Value;
        }

        public async Task SendEmail(SendgridBaseModel model)
        {
            SendGridMessage sendGridMessage = ConfigureSender(model);
            sendGridMessage.SetTemplateData(model);
            sendGridMessage.SetTemplateId(model.TemplateId);

            await SendEmail(sendGridMessage, model);
        }

        private SendGridMessage ConfigureSender(SendgridBaseModel model)
        {
            SendGridMessage sendGridMessage = new();
            string senderName = _sendgridSettings.ShopName;
            sendGridMessage.SetFrom(_sendgridSettings.SenderEmail, senderName);

            string receiverEmail = model.Email;
            sendGridMessage.AddTo(receiverEmail);

            return sendGridMessage;
        }

        private async Task SendEmail(SendGridMessage sendGridMessage, SendgridBaseModel model)
        {
            SendGridClient client = new(_sendgridSettings.ApiKey);
            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Log.Information("Email sent successfully to: " + model.Email);
            }
            else
            {
                Log.Warning("Email failed to send to: " + model.Email);
            }
        }

    }
}