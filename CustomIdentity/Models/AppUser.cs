using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CustomIdentity.Models
{
    [Table("AppUsers")]
    public class AppUser
    {
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("PasswordHash")]
        public string PasswordHash { get; set; }
        [Column("RoleName")]
        public string RoleName { get; set; }
        public string ConfirmPassword { get; set; }

        public AppUser(string name, string email, string password)
        {
            Name = name;
            Email = email;
            PasswordHash = password;
        }
        public AppUser(string email, string password)
        {
            Email = email;
            PasswordHash = password;
        }
        public AppUser()
        {

        }
    }
}
