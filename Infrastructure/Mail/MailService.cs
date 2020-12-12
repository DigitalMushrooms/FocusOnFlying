using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace FocusOnFlying.Infrastructure.Mail
{
    public class MailService : IMailService
    {
        private readonly MailConfiguration _mailConfiguration;

        public MailService(IOptions<MailConfiguration> options)
        {
            _mailConfiguration = options.Value;
        }

        public async Task WyslijWadomoscEmail(string adresEmail, string temat, string tresc)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailConfiguration.Mail);
            email.To.Add(MailboxAddress.Parse(adresEmail));
            email.Subject = temat;
            var builder = new BodyBuilder();
            builder.HtmlBody = tresc;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailConfiguration.Host, _mailConfiguration.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailConfiguration.Mail, _mailConfiguration.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
