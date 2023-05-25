using System.Text.Json.Serialization;

namespace HitecService.Models.DTO.Privileges;

public class PrivilegesDto
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("rol_id")] public int RolId { get; set; }

    [JsonPropertyName("menu_configuration_id")]
    public int MenuConfigurationId { get; set; }

    [JsonPropertyName("can_create")] public bool CanCreate { get; set; }

    [JsonPropertyName("can_read")] public bool CanRead { get; set; }

    [JsonPropertyName("can_delete")] public bool CanDelete { get; set; }

    [JsonPropertyName("can_edit")] public bool CanEdit { get; set; }

    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("deleted_at")] public DateTime? DeletedAt { get; set; }
}