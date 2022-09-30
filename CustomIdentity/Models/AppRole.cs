using CustomORM;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomIdentity.Models
{
    [Related(typeof(AppUser))]
    [Table("AppRoles")]
    public class AppRole : IdentityRole<int>
    {
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }

        public AppRole(string name)
        {
            Name = name;
        }
        public AppRole()
        {
           
        }
    }
}
