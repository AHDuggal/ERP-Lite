using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Settings;

public sealed class AzureBlobStorageSettings
{
    public const string SectionName = "AzureBlobStorage";

    public string ConnectionString { get; set; }
        = string.Empty;

    public string ProfileImagesContainer { get; set; }
        = "profile-images";
}