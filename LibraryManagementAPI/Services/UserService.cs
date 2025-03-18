using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class UserService
    {
        private readonly LibraryDbContext _context;

        public UserService(LibraryDbContext context)
        {
            _context = context;
        }

        // ✅ Get all users
        public async Task<List<User>> GetAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // ✅ Get user by username
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        // ✅ Get user by email
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // ✅ Create a new user
        public async Task CreateAsync(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
