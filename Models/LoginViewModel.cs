using System.ComponentModel.DataAnnotations;

namespace SinemaArsivSitesi.Models
{
    public class LoginViewModel :BaseEntity
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        public string Password { get; set; }
    }
}

