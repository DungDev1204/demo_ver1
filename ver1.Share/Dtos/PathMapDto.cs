using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo_ver1.Share.Enums;

namespace demo_ver1.Share.Dtos;
public class PathMapDto
{
    public Guid Id { get; set; }
    public Guid AreaId { get; set; }
    public string? Name { get; set; }
    public string? UniqueName { get; set; }
    public PathMapType PathType { get; set; }
    public Guid StartPointId { get; set; }
    public Guid EndPointId { get; set; }
    public Guid SupportPoint1Id { get; set; }
    public Guid SupportPoint2Id { get; set; }
    public Guid SupportPoint3Id { get; set; }
    public Guid SupportPoint4Id { get; set; }
}
