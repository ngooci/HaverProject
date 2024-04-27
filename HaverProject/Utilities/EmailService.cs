using HaverProject.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using HaverProject.ViewModel;

namespace CateringManagement.Utilities
{
    /// <summary>
    /// This implements the IEmailService from
    /// Microsoft.AspNetCore.Identity.UI.Services for the Identity System
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IEmailConfiguration _EmailConfiguration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IEmailConfiguration EmailConfiguration, ILogger<EmailSender> logger)
        {
            _EmailConfiguration = EmailConfiguration;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously builds and sends a message to a single recipient.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string Email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(Email, Email));
            message.From.Add(new MailboxAddress(_EmailConfiguration.SmtpFromName, _EmailConfiguration.SmtpUsername));

            message.Subject = subject;
            //We will say we are sending HTML. But there are options for plaintext etc. 
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage
            };
            try
            {
                //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                using var EmailClient = new SmtpClient();
                //The last parameter here is to use SSL (Which you should!)
                EmailClient.Connect(_EmailConfiguration.SmtpServer, _EmailConfiguration.SmtpPort, false);

                //Remove any OAuth functionality as we won't be using it. 
                EmailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                EmailClient.Authenticate(_EmailConfiguration.SmtpUsername, _EmailConfiguration.SmtpPassword);

                await EmailClient.SendAsync(message);

                EmailClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
            }
        }
    }
}
