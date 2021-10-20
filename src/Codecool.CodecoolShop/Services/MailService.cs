using Codecool.CodecoolShop.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class MailService: IMailService
    {
        public async Task Execute(string fromEmail, EmailConfirmation model)
        {
            var apiKey = "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";
            var templateId = "d-71a1079ff2da488b8c1dda86487ba50d";
            var client = new SendGridClient(apiKey);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetTemplateId(templateId);

            var senderName = "Codecool Shop";
            sendGridMessage.SetFrom(fromEmail, senderName);

            string receiverName = model.FullName;
            sendGridMessage.AddTo(model.Email, receiverName);

            sendGridMessage.SetTemplateData(model);

            var response = await client.SendEmailAsync(sendGridMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent successfully to: " + model.Email);
            }
        }
    }
}
