using System.Text.Json.Serialization;

namespace HitecService.Models.Requests.MenuConfiguration;

public class RequestUpdateMenuConfiguration
{
    [JsonPropertyName("menu_name")] public string MenuName { get; set; } = string.Empty;

    [JsonPropertyName("page_name")] public string PageName { get; set; } = string.Empty;
}