using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace demo_ver1.Share.Dtos;
public class ParameterGroupDto
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public string? Name { get; set; }
    public bool HasChildren { get; set; }

    [JsonIgnore]
    public IList<ParameterGroupDto>? Children { get; set; }
}
