using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Exceptions;

public class BusinessRuleException : BaseException
{
    public BusinessRuleException(string message)
        : base("BusinessRuleViolation", message)
    {
    }
}
