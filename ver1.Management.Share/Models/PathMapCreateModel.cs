using demo_ver1.Share.Enums;
using System.ComponentModel.DataAnnotations;


namespace demo_ver1.Management.Share.Models;
public class PathMapCreateModel
{
    [Required]
    public Guid AreaId { get; set; }

    [Required]
    public PathMapType PathType { get; set; }

    [Required]
    public double StartPositionX { get; set; }

    [Required]
    public double StartPositionY { get; set; }

    [Required]
    public double EndPositionX { get; set; }

    [Required]
    public double EndPositionY { get; set; }

    [Required]
    public double Support1PositionX { get; set; }

    [Required]
    public double Support1PositionY { get; set; }

    [Required]
    public double Support2PositionX { get; set; }

    [Required]
    public double Support2PositionY { get; set; }

    [Required]
    public double Support3PositionX { get; set; }

    [Required]
    public double Support3PositionY { get; set; }

    [Required]
    public double Support4PositionX { get; set; }

    [Required]
    public double Support4PositionY { get; set; }
}