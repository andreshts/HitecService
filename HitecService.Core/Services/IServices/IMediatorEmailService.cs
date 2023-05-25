namespace HitecService.Core.Services.IServices;

public interface IMediatorEmailService
{
    Task<bool> SendEmail(string to, string from, string password, string smtp, int port, string body, string subject);
}