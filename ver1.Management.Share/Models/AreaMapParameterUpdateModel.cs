

using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class AreaMapParameterUpdateModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public double MapOriginX { get; set; }

    [Required]
    public double MapOriginY { get; set; }

    [Required]
    [Range(0.000001, Double.MaxValue)]
    public double MapResolution { get; set; }
}

