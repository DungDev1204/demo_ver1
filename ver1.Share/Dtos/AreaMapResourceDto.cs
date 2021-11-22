using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace demo_ver1.Share.Dtos;
public class AreaMapResourceDto
{
    [JsonPropertyName("areaMapId")]
    public Guid AreaMapId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("contentType")]
    public string? ContentType { get; set; }

    [JsonPropertyName("fileUrl")]
    public string? FileUrl { get; set; }
}
