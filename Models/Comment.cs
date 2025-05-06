using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SinemaArsivSitesi.Models
{
    public class Comment : BaseEntity
    {

        public string UserId { get; set; }
        public IdentityUser User { get; set; } = null!;

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
