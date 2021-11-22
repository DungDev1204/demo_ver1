using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_ver1.Share.Dtos;
public class AreaMapResourceImageDto
{
    public Guid AreaMapId { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? ContentType { get; set; }
    public short Width { get; set; }
    public short Height { get; set; }
}