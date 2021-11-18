using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using System;
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

        public async Task SendEmail(OrderEmailConfirmation model, EmailTemplates emailTemplateOption)
        {
            var client = new SendGridClient(SendgridSettings.ApiKey);

            var sendGridMessage = new SendGridMessage();

            var senderName = "Codecool Shop";
            sendGridMessage.SetFrom(SendgridSettings.SenderEmail, senderName);

            string receiverName = model.FullName;
            sendGridMessage.AddTo(model.Email, receiverName);

            sendGridMessage.SetTemplateData(model);

            string emailTemplate = GetEmailTemplate(emailTemplateOption);
            sendGridMessage.SetTemplateId(emailTemplate);

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

        private string GetEmailTemplate(EmailTemplates option)
        {
            switch (option)
            {
                case EmailTemplates.AccountConfirmation:
                    return SendgridSettings.AccountConfirmationTemplateId;
                case EmailTemplates.OrderConfirmation:
                    return SendgridSettings.OrderConfirmationTemplateId;
                default:
                    throw new ArgumentNullException();
        }
    }
}
}