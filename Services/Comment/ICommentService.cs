using SinemaArsivSitesi.Models;
using Microsoft.EntityFrameworkCore;
using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Services.Comments;

namespace SinemaArsivSitesi.Services.Comments
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsByFilmIdAsync(int filmId);
        Task<Comment> GetCommentByIdAsync(int id);
        Task AddCommentAsync(Comment yorum);
        Task UpdateCommentAsync(Comment yorum);
        Task DeleteCommentAsync(int id);
    }
}
