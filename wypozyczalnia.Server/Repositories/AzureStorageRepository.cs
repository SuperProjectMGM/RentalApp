using System.Configuration;
using wypozyczalnia.Server.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace wypozyczalnia.Server.Repositories;

public class AzureStorageRepository: IStorageInterface
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly string _containerName;

    public AzureStorageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration["AZURE_CONNECTION_STRING"]!;
        _containerName = "images";
    }

    // Should I use await?
    public async Task<Uri> GetUriToStorage()
    {
        var blobContainerClient = new BlobContainerClient(_connectionString, _containerName);

        // Should I specify here containers name?
        var blobSasBuilder = new BlobSasBuilder(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write, DateTimeOffset.Now.AddMinutes(10));
        blobSasBuilder.Resource = "c";
        blobSasBuilder.BlobContainerName = _containerName;

        Uri sasUri = blobContainerClient.GenerateSasUri(blobSasBuilder);

        return sasUri;
    }
}