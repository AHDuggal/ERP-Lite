using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERPLite.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadProfileImageAsync(
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        string blobUrl,
        CancellationToken cancellationToken);
}