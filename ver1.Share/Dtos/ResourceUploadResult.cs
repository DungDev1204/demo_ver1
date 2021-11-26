
using System.Text.Json.Serialization;
namespace demo_ver1.Share.Dtos;
public class ResourceUploadResult
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("type")]
	public string? Type { get; set; }

	[JsonPropertyName("status")]
	public string? Status { get; set; }
}