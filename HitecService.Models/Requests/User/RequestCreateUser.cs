using System.Text.Json.Serialization;

namespace HitecService.Models.Requests.User;

public class RequestCreateUser
{
    [JsonPropertyName("rol_id")] public int RolId { get; set; }

    [JsonPropertyName("user_name")] public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")] public string Password { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("last_name")] public string? LastName { get; set; }

    [JsonPropertyName("phone")] public string? Phone { get; set; }

    [JsonPropertyName("address")] public string? Address { get; set; }

    [JsonPropertyName("observation")] public string? Observation { get; set; }

    [JsonPropertyName("status")] public bool Status { get; set; }
}