using demo_ver1.Share.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo_ver1.Management.Share.Models;
public class ParameterCreateModel
{
    [Required]
    public Guid GroupId { get; set; }

    [Required]
    [MinLength(4)]
    [RegularExpression("^[a-zA-Z0-9-_]*$", ErrorMessage = "Tên parameter chỉ chứa các ký tự alphabet, số và '-', '_'")]
    public string? Name { get; set; }

    [Required]
    public string? Value { get; set; }

    [Required]
    public ParameterType Type { get; set; }

    [JsonIgnore]
    public string StringValue
    {
        get => Value ?? "";
        set { if (Type == ParameterType.String) Value = value; }
    }

    [JsonIgnore]
    public int IntValue
    {
        get => Type == ParameterType.Integer && int.TryParse(Value, out int v) ? v : 0;
        set { if (Type == ParameterType.Integer) Value = value.ToString(); }
    }

    [JsonIgnore]
    public double DoubleValue
    {
        get => Type == ParameterType.Double && double.TryParse(Value, out double v) ? v : 0;
        set { if (Type == ParameterType.Double) Value = value.ToString(); }
    }

    [JsonIgnore]
    public bool BooleanValue
    {
        get => Type == ParameterType.Boolean && bool.TryParse(Value, out bool v) && v;
        set { if (Type == ParameterType.Boolean) Value = value.ToString(); }
    }
}