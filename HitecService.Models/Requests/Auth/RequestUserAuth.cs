namespace HitecService.Models.Requests.Auth;

public class RequestUserAuth
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}