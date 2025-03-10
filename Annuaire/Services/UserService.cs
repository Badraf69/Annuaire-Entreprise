using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
            //User newUser = user;
            //MessageBox.Show($"nouvelle utilisateur : {newUser}");
            
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

    public async Task<List<User>>GetUsersAsync()
    {
        var response = await _httpClient.GetAsync($"GetAllUsers");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<User>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
    /**
     * Fonction pour la suppression d'un utilisateur à partir de son id
     * @param int => id
     * @return int => code reponse serveur
     */
    public async Task<int> DeleteUserAsync(int userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"DeleteUser/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"$Erreur lors de la suppresion de l'utilisateur : {errorMessage}");
                return (int)response.StatusCode;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}