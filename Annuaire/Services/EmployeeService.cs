using System.Net.Http;
using System.Text.Json;
using AnnuaireModel;

namespace Annuaire.Services;

public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5245");
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var response = await _httpClient.GetAsync("GetAllEmployees");
        response.EnsureSuccessStatusCode();
        
        var json =await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Employee>>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
    }

    public async Task<bool> DeleteEmployeeAsync(int employeeId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"DeleteEmployee?employeeId={employeeId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur de suppression : {errorMessage}");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}