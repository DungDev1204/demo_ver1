using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_ver1.Share.Dtos;
public class PointMapDto
{
    public Guid Id { get; set; }
    public Guid AreaId { get; set; }
    public string? Name { get; set; }
    public string? UniqueName { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public bool IsSupport { get; set; }
    public Guid LinkedPointId { get; set; }
}

