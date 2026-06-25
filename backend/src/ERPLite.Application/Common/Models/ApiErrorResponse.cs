using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Models;

public sealed class ApiErrorResponse
{
    public bool Success { get; init; } = false;

    public int StatusCode { get; init; }

    public string ErrorCode { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;

    public List<string> Errors { get; init; } = [];
}
