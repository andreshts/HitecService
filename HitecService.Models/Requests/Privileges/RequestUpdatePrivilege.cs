using System.Text.Json.Serialization;

namespace HitecService.Models.Requests.Privileges;

public class RequestUpdatePrivilege
{
    [JsonPropertyName("rol_id")] public int? RolId { get; set; }

    [JsonPropertyName("menu_configuration_id")]
    public int? MenuConfigurationId { get; set; }

    [JsonPropertyName("can_create")] public bool? CanCreate { get; set; }

    [JsonPropertyName("can_read")] public bool? CanRead { get; set; }

    [JsonPropertyName("can_delete")] public bool? CanDelete { get; set; }

    [JsonPropertyName("can_edit")] public bool? CanEdit { get; set; }
}