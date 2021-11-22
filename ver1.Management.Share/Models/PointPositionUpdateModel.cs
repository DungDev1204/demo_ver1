using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class PointPositionUpdateModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public double X { get; set; }

    [Required]
    public double Y { get; set; }

    [Required]
    public Guid LinkedId { get; set; }
}

