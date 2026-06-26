using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Organizations.DTOs;

public sealed class UpdateOrganizationRequest
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;
}