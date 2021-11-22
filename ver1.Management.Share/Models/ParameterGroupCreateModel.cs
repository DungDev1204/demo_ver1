using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class ParameterGroupCreateModel
{
    [Required]
    public Guid ParentId { get; set; }

    [Required]
    [MinLength(4)]
    [RegularExpression("^[a-zA-Z0-9-_]*$", ErrorMessage = "Tên parameter chỉ chứa các ký tự alphabet, số và '-', '_'")]
    public string? Name { get; set; }
}
