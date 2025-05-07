using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering; 
using SinemaArsivSitesi.Services.Comments;

namespace SinemaArsivSitesi.Models
{
    public class UserFavoriteMovie :BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedById { get; set; }

        public virtual Movie Movie { get; set; }  
    }

}
