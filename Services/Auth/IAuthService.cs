using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Auth
{
    public interface IAuthService 
    {

        Task<bool> Login(string email, string password);
        Task<bool> Register(string email, string password);
        Task<bool> Logout();
        Task<bool> ResetPassword(string email);
        Task<bool> ChangePassword(string oldPassword, string newPassword);
        Task<bool> UpdateProfile(string name, string surname, string email);
        Task<bool> DeleteAccount();
    }
}
