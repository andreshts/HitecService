using HitecService.Core.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace HitecService.Core.Services;

public class MediatorEmailService : IMediatorEmailService
{

    public async Task<bool> SendEmail(string to, string from, string password, string host, int port, string body, string subject)
    {
        try
        {
            var email = new MimeMessage();
            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            email.Sender  = MailboxAddress.Parse(from);
            email.Subject = subject;
            email.Body    = builder.ToMessageBody();
            email.To.Add(MailboxAddress.Parse(to));

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(from, password);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }
}