using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Exceptions;

public sealed class ValidationException : BaseException
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(string message)
        : base("ValidationError", message)
    {
        Errors = new List<string> { message };
    }

    public ValidationException(IEnumerable<string> errors)
        : base("ValidationError", "Validation failed.")
    {
        Errors = errors.ToList();
    }
}