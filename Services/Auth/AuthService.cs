using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            return user.PasswordHash == HashPassword(password);
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                Email = email,
                PasswordHash = HashPassword(password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.PasswordHash = HashPassword("123456"); 
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public Task<bool> UpdateProfile(string name, string surname, string email)
        {
            throw new NotImplementedException();
        }
    }
}