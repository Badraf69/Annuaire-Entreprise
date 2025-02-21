using System.ComponentModel.DataAnnotations;

namespace AnnuaireModel
{

    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string PasswordHash { get; set; }
        
        public User(){}

        public User(
            int id,
            string userName,
            string passwordHash
            )
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
           
        }

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
    
}