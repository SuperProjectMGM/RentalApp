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

    // Should I use await?
    public async Task<Uri> GetUriToStorage(string containerName)
    {
        var blobContainerClient = new BlobContainerClient(_connectionString, containerName);

        // Should I specify here containers name?
        var blobSasBuilder = new BlobSasBuilder(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write, DateTimeOffset.Now.AddMinutes(10));
        blobSasBuilder.Resource = "c";
        blobSasBuilder.BlobContainerName = containerName;

        Uri sasUri = blobContainerClient.GenerateSasUri(blobSasBuilder);

        return sasUri;
    }
}