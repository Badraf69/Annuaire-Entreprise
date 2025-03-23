using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnnuaireModel;

// namespace AnnuaireAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AuthController : ControllerBase
//     {
//         private readonly IConfiguration _configuration;
//         private AppDbContext _appDbContext;
//         private User _user;
//
//         public AuthController(User user, IConfiguration configuration)
//         {
//             _configuration = configuration;
//             _user = user;
//         }
//
//         [HttpPost("login")]
//         public IActionResult Login([FromBody] User user)
//         {
//             // Chercher l'utilisateur dans la base de données
//             var existingUser = _appDbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);
//             if (existingUser == null)
//             {
//                 return Unauthorized(new { message = "Utilisateur introuvable" });
//             }
//
//             // Vérifier si le mot de passe correspond
//             bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash);
//             if (!isPasswordValid)
//             {
//                 return Unauthorized(new { message = "Mot de passe incorrect" });
//             }
//
//             // Générer un JWT si les informations sont valides
//             var claims = new[]
//             {
//                 new Claim(JwtRegisteredClaimNames.Sub, existingUser.UserName),
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//             };
//
//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//
//             var token = new JwtSecurityToken(
//                 issuer: _configuration["Jwt:Issuer"],
//                 audience: _configuration["Jwt:Audience"],
//                 claims: claims,
//                 expires: DateTime.Now.AddHours(1),
//                 signingCredentials: creds);
//
//             var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
//             return Ok(new { token = tokenString });
//         }
//     }
// }