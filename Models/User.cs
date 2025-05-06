using System.ComponentModel.DataAnnotations;

namespace SinemaArsivSitesi.Models
{
    public class User : BaseEntity
    {
       
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "User"; 

        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}