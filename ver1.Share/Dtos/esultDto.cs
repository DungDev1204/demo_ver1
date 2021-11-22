using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_ver1.Share.Dtos;
public class ResultDto
{
    public bool IsError { get; init; }
    public string? Message { get; init; }
}

public class ResultDto<T> : ResultDto
{
    public T? Result { get; init; }
}
