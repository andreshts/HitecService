using System.Text.Json.Serialization;

namespace HitecService.Models.DTO.Rol;

public class RoleDto
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;

    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("deleted_at")] public DateTime? DeletedAt { get; set; }

    [JsonPropertyName("associated_users")] public int AssociatedUsers { get; set; }

    [JsonPropertyName("permissions")] public int Permissions { get; set; }
}