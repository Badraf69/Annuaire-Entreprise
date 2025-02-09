using System.Net.Http;
using System.Text.Json;
using AnnuaireModel;
using Microsoft.Extensions.Logging;

namespace Annuaire.Services;

public class ServiceService
{
    
    private readonly HttpClient _httpClient;

    public ServiceService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5245");
    }
    public async Task<List<Service>> GetServicesAsync()
    {
        var response = await _httpClient.GetAsync("GetAllServices");
        response.EnsureSuccessStatusCode();
        
        var json =await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Service>>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
    }
}