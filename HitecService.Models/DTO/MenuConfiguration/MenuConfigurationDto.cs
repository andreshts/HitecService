using System.Text.Json.Serialization;

namespace HitecService.Models.DTO.MenuConfiguration;

public class MenuConfigurationDto
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("menu_name")] public string MenuName { get; set; } = string.Empty;

    [JsonPropertyName("page_name")] public string PageName { get; set; } = string.Empty;

    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("deleted_at")] public DateTime? DeletedAt { get; set; }
    [JsonPropertyName("can_create")] public bool CanCreate { get; set; }

    [JsonPropertyName("can_read")] public bool CanRead { get; set; }

    [JsonPropertyName("can_delete")] public bool CanDelete { get; set; }

    [JsonPropertyName("can_edit")] public bool CanEdit { get; set; }
}