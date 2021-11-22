using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class RobotCreateModel
{
    [Required]
    [MaxLength(255)]
    [MinLength(8)]
    public string? Name { get; set; }

    [Required]
    [RegularExpression(@"^[a-z0-9\-]*$", ErrorMessage = "Unique name chỉ gồm chữ cái thường, chữ số và dấu \"-\"")]
    [MaxLength(255)]
    [MinLength(8)]
    public string? UniqueName { get; set; }

    [Required]
    public Guid? RobotVersionId { get; set; }
}
