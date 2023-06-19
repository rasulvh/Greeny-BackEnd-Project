using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using ServiceLayer.Services.Interfaces;
using MailKit.Net.Smtp;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("rasulvh@code.edu.az"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("rasulvh@code.edu.az", "dwclllafbydrcafb");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
