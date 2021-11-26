
namespace demo_ver1.Share.Dtos;
public class ResponseAuthorizationDto
{
    public bool IsError { get; set; }
    public string? Message { get; set; }
    public string? DeviceCode { get; set; }
    public string? UserCode { get; set; }
    public string? VerificationUri { get; set; }
    public string? VerificationUriComplete { get; set; }
}