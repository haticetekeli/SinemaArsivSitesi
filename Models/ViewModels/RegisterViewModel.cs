using System.ComponentModel.DataAnnotations;
using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Models.ViewModels
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}