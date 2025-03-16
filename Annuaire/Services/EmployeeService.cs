using System.Net.Http;
using System.Net.Http.Json;
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
            var response = await _httpClient.DeleteAsync($"DeleteEmployee/{employeeId}");
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

    public async Task<Employee> AddEmployee(Employee employee)
    {
        try
        {
            var response =  await _httpClient.PostAsJsonAsync($"AddEmployee", employee);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Employee>();
            }
            else
            {
               throw new Exception($"Erreur lors de l'ajout d'un employé : { response.ReasonPhrase}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"UpdateEmployee/{employee.Id}", employee);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Employee>();
            }
            else
            {
                throw new Exception($"Erreur lors de la modification d'un employé : { response.ReasonPhrase}");
            }
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

}