using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Exceptions;

public class ValidationException : BaseException
{
    public List<string> Errors { get; }

    public ValidationException(string message)
        : base("ValidationError", message)
    {
        Errors = new List<string> { message };
    }

    public ValidationException(List<string> errors)
        : base("ValidationError", "Validation failed.")
    {
        Errors = errors;
    }
}