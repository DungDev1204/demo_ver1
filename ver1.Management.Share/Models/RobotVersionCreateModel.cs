using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class RobotVersionCreateModel
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

    public IFormFile? ProjectionImage { get; set; }

    [Required]
    [Range(0, short.MaxValue)]
    public short MajorVersion { get; set; }

    [Required]
    [Range(0, short.MaxValue)]
    public short MinorVersion { get; set; }

    [Required]
    [Range(0, short.MaxValue)]
    public short PatchVersion { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public double Width { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public double Length { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public double Height { get; set; }
}

