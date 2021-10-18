using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class MailService: IMailService
    {
        public async Task Execute(string fromEmail, Dictionary<string, string> receiver)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetTemplateId("d-99b04ce5a622406c93061598ad62c852");
            sendGridMessage.SetFrom(fromEmail, "Codecool Shop");
            sendGridMessage.AddTo(receiver["email"], receiver["name"]);

            var response = await client.SendEmailAsync(sendGridMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent successfully to: " + receiver["email"]);
            }
        }
    }
}
