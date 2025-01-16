using System.Configuration;
using wypozyczalnia.Server.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace wypozyczalnia.Server.Repositories;

public class AzureStorageRepository: IStorageInterface
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public AzureStorageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration["AZURE_CONNECTION_STRING"]!;
    }

    public async Task<Uri> GetUriToStorage(string containerName)
    {
        var blobContainerClient = new BlobContainerClient(_connectionString, containerName);

        var blobSasBuilder = new BlobSasBuilder(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write | BlobContainerSasPermissions.Create
        | BlobContainerSasPermissions.Add, DateTimeOffset.Now.AddMinutes(10));
        blobSasBuilder.Resource = "c";
        blobSasBuilder.BlobContainerName = containerName;

        Uri sasUri = blobContainerClient.GenerateSasUri(blobSasBuilder);

        return sasUri;
    }
}