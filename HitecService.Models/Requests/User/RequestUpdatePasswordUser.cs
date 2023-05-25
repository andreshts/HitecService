using System.Text.Json.Serialization;

namespace HitecService.Models.Requests.User;

public class RequestUpdatePasswordUser
{
    [JsonPropertyName("password")] public string Password { get; set; } = string.Empty;

    [JsonPropertyName("new_password")] public string NewPassword { get; set; } = string.Empty;
}