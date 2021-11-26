using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class AreaMapCreateModel
{
    [Required]
    [MinLength(10)]
    public string? Name { get; set; }
}

