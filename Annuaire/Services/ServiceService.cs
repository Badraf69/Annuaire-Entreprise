using System.Net.Http;
using System.Net.Http.Json;
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

    public async Task<Service> GetServiceByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"GetServiceById?id={id}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Service>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        
    }

    public async Task<Service> AddServiceAsync(Service service)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"AddService", service);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Service>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
            }
            else
            {
                throw new Exception($"Erreur lors de l'ajout d'un service : {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}