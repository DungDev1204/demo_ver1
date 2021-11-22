using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class AreaMapNameUpdateModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    [MinLength(8)]
    public string? Name { get; set; }

    [Required]
    [RegularExpression(@"^[a-z0-9\-]*$", ErrorMessage = "Unique name chỉ gồm chữ cái thường, chữ số và dấu \"-\"")]
    [MaxLength(255)]
    [MinLength(8)]
    public string? UniqueName { get; set; }
}
