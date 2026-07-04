using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace ERPLite.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public string Description { get; set; }
        = string.Empty;
}
