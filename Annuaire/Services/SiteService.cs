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
    /**
     * Fonction pour la suppression d'un service à partir de son id
     * Gestion si tentative ce suppression alors q'un employe est associé à ce service
     * @param int => id
     * @return int => code reponse serveur
     */
    public async Task<int> DeleteSiteAsync(int serviceId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"DeleteSite/{serviceId}");
            if (response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage.Contains("SQLite Error 19"))
                {
                    int result = 600;
                    return result;
                }
                
                Console.WriteLine($"$Erreur de suppresion du site : {errorMessage}");
                return (int)response.StatusCode;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /**
     * Fonction pour recupérer un service via son id
     * @param int => id
     * @return json contenant le service
     */
    public async Task<Site> getSiteByIdAsync(int serviceId)
    {
        var response = await _httpClient.GetAsync($"GetSiteById/{serviceId}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Site>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        
    }

    public async Task<Site> UpdateSite(Site site)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"UpdateSite/{site.Id}", site);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Site>();
            }
            else
            {
                throw new Exception($"Erreur lors de la modification d'un site : { response.ReasonPhrase}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}