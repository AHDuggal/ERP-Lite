using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERPLite.Application.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string entity, string key)
        : base("NotFound", $"{entity} with id {key} was not found.")
    {
    }
}
