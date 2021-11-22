using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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