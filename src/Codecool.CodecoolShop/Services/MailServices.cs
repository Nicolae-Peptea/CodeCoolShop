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

        //public async Task SendOrderConfirmation(OrderEmailConfirmation model)
        //{
        //    SendGridClient client = new SendGridClient(SendgridSettings.ApiKey);

        //    SendGridMessage sendGridMessage = new SendGridMessage();

        //    string senderName = "Codecool Shop";
        //    sendGridMessage.SetFrom(SendgridSettings.SenderEmail, senderName);

        //    sendGridMessage.AddTo(model.Email);

        //    sendGridMessage.SetTemplateData(model);

        //    sendGridMessage.SetTemplateId(SendgridSettings.OrderConfirmationTemplateId);

        //    var response = await client.SendEmailAsync(sendGridMessage);
        //    if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        //    {
        //        Log.Information("Email sent successfully to: " + model.Email);
        //    }
        //    else
        //    {
        //        Log.Information("Email failed to send to: " + model.Email);
        //    }
        //}

        public async Task SendOrderConfirmation(SendgridOrderConfirmationModel model)
        {
            SendGridMessage sendGridMessage = ConfigureSender(model);
            sendGridMessage.SetTemplateData(model);
            sendGridMessage.SetTemplateId(SendgridSettings.OrderConfirmationTemplateId);

            await SendEmail(sendGridMessage, model);
        }

        private SendGridMessage ConfigureSender(SendgridBaseModel model)
        {
            SendGridMessage sendGridMessage = new SendGridMessage();
            string senderName = "Codecool Shop";
            sendGridMessage.SetFrom(SendgridSettings.SenderEmail, senderName);
            sendGridMessage.AddTo(model.Email);

            return sendGridMessage;
        }

        private async Task SendEmail(SendGridMessage sendGridMessage, SendgridBaseModel model)
        {
            SendGridClient client = new SendGridClient(SendgridSettings.ApiKey);
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