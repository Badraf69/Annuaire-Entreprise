using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using AnnuaireModel;

namespace Annuaire.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5245");
    }

    public async Task<User> AddUser(User user)
    {
        try
        {
            User newUser = user;
            MessageBox.Show($"nouvelle utilisateur : {newUser}");
            
            var response = await _httpClient.PostAsJsonAsync($"AddUser", user);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                throw new Exception($"Erreur lors de l'ajout d'un utilisateur : {response.ReasonPhrase}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}