using System.Text.Json.Serialization;

namespace HitecService.Models.DTO.Privileges;

public class PrivilegeMenuConfigurationDto
{
    [JsonPropertyName("privilege_id")] public int? PrivilegeId { get; set; }

    [JsonPropertyName("menu_configuration_id")]
    public int? MenuConfigurationId { get; set; }

    [JsonPropertyName("menu_name")] public string? MenuName { get; set; } = string.Empty;

    [JsonPropertyName("page_name")] public string PageName { get; set; } = string.Empty;

    [JsonPropertyName("can_create")] public bool CanCreate { get; set; }

    [JsonPropertyName("can_read")] public bool CanRead { get; set; }

    [JsonPropertyName("can_delete")] public bool CanDelete { get; set; }

    [JsonPropertyName("can_edit")] public bool CanEdit { get; set; }
}