using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class MailServices : IMailService
    {
        public async Task SendOrderConfirmationEmail(OrderEmailConfirmation model, SendgridSettings sendgridSettings,
            EmailTemplates emailTemplateOption = null)
        {
            var client = new SendGridClient(sendgridSettings.ApiKey);

            var sendGridMessage = new SendGridMessage();

            var senderName = "Codecool Shop";
            sendGridMessage.SetFrom(sendgridSettings.SenderEmail, senderName);

            string receiverName = model.FullName;
            sendGridMessage.AddTo(model.Email, receiverName);

            sendGridMessage.SetTemplateData(model);

            sendGridMessage.SetTemplateId(sendgridSettings.OrderConfirmationTemplateId);

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

        //private string GetEmailTemplate(int option)
        //{
        //    switch (option)
        //    {
        //        if option == 1:
        //            return 
        //        default:
        //            break;
        //    }
        //}
    }
}