using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message)
        : base("Forbidden", message)
    {
    }
}
