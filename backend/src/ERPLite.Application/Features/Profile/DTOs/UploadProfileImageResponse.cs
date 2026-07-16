using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Profile.DTOs;

public sealed class UploadProfileImageResponse
{
    public string ImageUrl { get; set; }
        = string.Empty;
}