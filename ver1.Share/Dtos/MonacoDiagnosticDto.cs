
using Microsoft.CodeAnalysis.Text;

namespace demo_ver1.Share.Dtos;
public class MonacoDiagnosticDto
{
    public LinePosition Start { get; init; }
    public LinePosition End { get; init; }
    public string? Message { get; init; }
    public int Severity { get; init; }
}
