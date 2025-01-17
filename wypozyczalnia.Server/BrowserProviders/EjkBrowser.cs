using System.CodeDom;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.BrowserProviders;

public class EjkBrowser
{
    private readonly HttpClient _httpClient;

    public EjkBrowser(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task RentalCompleted(Rental rental)
    {
        var slug = rental.Slug;
        string[] arr = slug.Split("_");
        var externalOfferId = int.Parse(arr[0]);
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://user-api-dotnet.azurewebsites.netapi/cars/rental/confirmation");
            request.Content = JsonContent.Create(externalOfferId);
            
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorContent}");
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
        }
    }

    public async Task AcceptReturn(Rental rental)
    {
        var slug = rental.Slug;
        string[] arr = slug.Split("_");
        var externalOfferId = int.Parse(arr[0]);
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://user-api-dotnet.azurewebsites.netapi/cars/return/confirmation");
            request.Content = JsonContent.Create(externalOfferId);
            
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorContent}");
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
        }
    }
}