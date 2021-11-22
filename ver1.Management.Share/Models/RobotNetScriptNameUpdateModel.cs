using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class RobotNetScriptNameUpdateModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(4)]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Tên script chỉ chứa các ký tự alphabet, số")]
    public string? Name { get; set; }
}
