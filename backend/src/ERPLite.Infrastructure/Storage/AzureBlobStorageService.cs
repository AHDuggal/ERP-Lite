using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ERPLite.Application.Common.Interfaces;
using ERPLite.Application.Common.Settings;
using Microsoft.Extensions.Options;

namespace ERPLite.Infrastructure.Storage;

public sealed class AzureBlobStorageService
    : IFileStorageService
{
    private readonly AzureBlobStorageSettings _settings;

    public AzureBlobStorageService(
        IOptions<AzureBlobStorageSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<string> UploadProfileImageAsync(
        Stream stream,
        string fileName,
        string contentType,        
        CancellationToken cancellationToken)
    {
        var blobServiceClient =
            new BlobServiceClient(
                _settings.ConnectionString);

        var container =
    blobServiceClient.GetBlobContainerClient(
        _settings.ProfileImagesContainer);

        await container.CreateIfNotExistsAsync(
            PublicAccessType.None,
            cancellationToken: cancellationToken);

        var blobName =
            $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        var blob =
            container.GetBlobClient(blobName);

        await blob.UploadAsync(
            stream,
            new BlobHttpHeaders
            {
                ContentType = contentType
            },
            cancellationToken: cancellationToken);

        return blob.Uri.ToString();
    }

    public async Task DeleteAsync(
        string blobUrl,
        CancellationToken cancellationToken)
    {
        var blob =
            new BlobClient(
                new Uri(blobUrl));

        await blob.DeleteIfExistsAsync(
            cancellationToken: cancellationToken);
    }
}
