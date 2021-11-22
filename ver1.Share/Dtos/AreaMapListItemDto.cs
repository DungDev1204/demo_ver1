using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_ver1.Share.Dtos;
public class AreaMapListItemDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UniqueName { get; set; }
    public string? MapImageUrl { get; set; }
}

