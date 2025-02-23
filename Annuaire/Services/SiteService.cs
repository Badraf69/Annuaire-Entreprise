using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using AnnuaireModel;
using Microsoft.Extensions.Logging;

namespace Annuaire.Services;

public class SiteService
{
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
    public async Task<Site> AddSite(Site site)
    {
        try
        {
            var response =  await _httpClient.PostAsJsonAsync($"AddSite", site);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Site>();
            }
            else
            {
                throw new Exception($"Erreur lors de l'ajout d'un site : { response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"La méthode AddSite est en erreur :{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        return null;
    }
}