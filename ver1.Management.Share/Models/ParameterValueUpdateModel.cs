using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace demo_ver1.Management.Share.Models;
public class ParameterValueUpdateModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? Value { get; set; }

    [JsonIgnore]
    public string StringValue
    {
        get => Value ?? "";
        set { Value = value; }
    }

    [JsonIgnore]
    public int IntValue
    {
        get => int.TryParse(Value, out int v) ? v : 0;
        set { Value = value.ToString(); }
    }

    [JsonIgnore]
    public double DoubleValue
    {
        get => double.TryParse(Value, out double v) ? v : 0;
        set { Value = value.ToString(); }
    }

    [JsonIgnore]
    public bool BooleanValue
    {
        get => bool.TryParse(Value, out bool v) && v;
        set { Value = value.ToString(); }
    }
}

