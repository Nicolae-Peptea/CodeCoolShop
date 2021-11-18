using Codecool.CodecoolShop.Helpers;
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
        public SendgridSettings SendgridSettings { get; private set; }

        public MailServices(IOptions<SendgridSettings> sendgridSettings)
        {
            SendgridSettings = sendgridSettings.Value;
        }

        public async Task SendOrderConfirmation(SendgridOrderConfirmationModel model)
        {
            SendGridMessage sendGridMessage = ConfigureSender(model);
            sendGridMessage.SetTemplateData(model);
            sendGridMessage.SetTemplateId(SendgridSettings.OrderConfirmationTemplateId);

            await SendEmail(sendGridMessage, model);
        }

        public async Task SendAccountRegisterConfirmation(SendgridAccountConfirmationModel model)
        {
            SendGridMessage sendGridMessage = ConfigureSender(model);
            sendGridMessage.SetTemplateData(model);
            sendGridMessage.SetTemplateId(SendgridSettings.AccountConfirmationTemplateId);

            await SendEmail(sendGridMessage, model);
        }

        private SendGridMessage ConfigureSender(SendgridBaseModel model)
        {
            SendGridMessage sendGridMessage = new();
            string senderName = "Codecool Shop";
            sendGridMessage.SetFrom(SendgridSettings.SenderEmail, senderName);
            sendGridMessage.AddTo(model.Email);

            return sendGridMessage;
        }

        private async Task SendEmail(SendGridMessage sendGridMessage, SendgridBaseModel model)
        {
            SendGridClient client = new(SendgridSettings.ApiKey);
            var response = await client.SendEmailAsync(sendGridMessage);
            
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Log.Information("Email sent successfully to: " + model.Email);
            }
            else
            {
                Log.Information("Email failed to send to: " + model.Email);
            }
        }
    }
}