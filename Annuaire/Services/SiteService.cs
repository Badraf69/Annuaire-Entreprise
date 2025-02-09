using System.Net.Http;
using System.Text.Json;
using AnnuaireModel;
using Microsoft.Extensions.Logging;

namespace Annuaire.Services;

public class SiteService
{
    private readonly ILogger<SiteService> _logger;
    private readonly HttpClient _httpClient;

    public SiteService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5245");
    }
    public async Task<List<Site>> GetSitesAsync()
    {
        var response = await _httpClient.GetAsync("GetAllSites");
        response.EnsureSuccessStatusCode();
        
        var json =await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Site>>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
    }
}