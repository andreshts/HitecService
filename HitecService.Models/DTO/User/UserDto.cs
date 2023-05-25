using System.Text.Json.Serialization;

namespace HitecService.Models.DTO.User;

public class UserDto
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("rol_id")] public int RolId { get; set; }

    [JsonPropertyName("is_affiliate")] public sbyte IsAffiliate { get; set; }
    [JsonPropertyName("rol_name")] public string RolName { get; set; } = string.Empty;

    [JsonPropertyName("user_name")] public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("last_name")] public string? LastName { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")] public string? Phone { get; set; }

    [JsonPropertyName("address")] public string? Address { get; set; }

    [JsonPropertyName("observation")] public string? Observation { get; set; }

    [JsonPropertyName("status")] public bool Status { get; set; }

    [JsonPropertyName("token")] public string Token { get; set; } = string.Empty;

    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("deleted_at")] public DateTime? DeletedAt { get; set; }
}