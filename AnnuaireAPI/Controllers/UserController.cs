using System.Diagnostics.Metrics;
using AnnuaireModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireAPI.Controllers;
[ApiController]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext appContext;
    private readonly User _user;
    

    public UserController(ILogger<UserController> logger, AppDbContext context)
    {
        _logger = logger;
        appContext = context;
    }
    
    //Route API pour récupérer tous les users
    [HttpGet("GetAll[controller]s", Name = "GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await appContext.Users.ToListAsync();
            if (users.Count == 0)
            {
                return NotFound(new { message = "Aucun utilisateur n'existe." });
            }
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du getUsers: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");

        }
    }
    
    //Route API pour récupérer un user via son id
    [HttpGet("Get[controller]ById/{id}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = appContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "Utilisateur non trouvé." });
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du getEmployeeById: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");

        }
    }
    
    //Route API pour l'ajout d'un user
    [HttpPost("Add[controller]", Name = "AddUser")] //ValidateAntiForgeryToken
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        Console.WriteLine("usercontroller");
        Console.WriteLine(user.UserName);
        Console.WriteLine(user.PasswordHash);
        try
        {
            if (user == null)
            {
                return BadRequest(new { message = "Donnée incorrect." });
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await appContext.Users.AddAsync(user);
            await appContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors de l'ajout d'un utilisateur: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    
    //Route API pour supprimer un utilisateur
    [HttpDelete("Delete/{id}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = appContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new {message="Utilisateur non trouvé"});
            }

            int userCount = await appContext.Users.CountAsync();
            if (userCount <= 1)
            {
                return StatusCode(400, new { message = "Vous ne pouvez pas supprimer le dernier utilisateur." });
            }
            appContext.Users.Remove(user);
            await appContext.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors du DeleteEmployee: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
    
    //Route API pour modifier un utilisateur 
    [HttpPut("Put[controller]", Name = "UpdateUser")]
    public async Task<IActionResult> UpdateUser(int id, User updatedUser)
    {
        try
        {
            var existingUser = appContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(new {message="Utilisateur non trouvé"}); 
            }
            
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);;
            appContext.Update(existingUser);
            await appContext.SaveChangesAsync();
            return Ok(existingUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "erreur lors de la modification du mot de passe: {Message}", ex.Message);
            return StatusCode(500,$"Erreur interne du serveur: {ex.Message}");
        }
    }
}