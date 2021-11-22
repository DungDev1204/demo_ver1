﻿using System.ComponentModel.DataAnnotations;

namespace demo_ver1.Management.Share.Models;
public class RobotNetScriptCreateModel
{
    [Required]
    [MinLength(4)]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Tên parameter chỉ chứa các ký tự alphabet, số")]
    public string? Name { get; set; }
}
