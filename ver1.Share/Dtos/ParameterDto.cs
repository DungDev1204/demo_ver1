using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo_ver1.Share.Enums;

namespace demo_ver1.Share.Dtos;
public class ParameterDto
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
    public ParameterType Type { get; set; }
}

