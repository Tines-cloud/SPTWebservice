using Microsoft.EntityFrameworkCore;
using SPTUserService.Data;
using SPTUserService.Models;

namespace SPTUserService.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.User
                .Include(u => u.UserPreference)
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.User
                .Include(u => u.UserPreference)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            if (user.RoleId != default(int))
            {
                var existingRole = await _context.Role.FindAsync(user.RoleId);
                if (existingRole != null)
                {
                    user.Role = existingRole;
                }
            }

            if (user.UserPreference != null)
            {
                _context.UserPreference.Add(user.UserPreference);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.User
               .Include(u => u.UserPreference)
               .Include(u => u.Role)
               .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;

                if (user.UserPreference != null)
                {
                    existingUser.UserPreference.DefFocusTime = user.UserPreference.DefFocusTime;
                    existingUser.UserPreference.DefBreakTime = user.UserPreference.DefBreakTime;
                    existingUser.UserPreference.Notifications = user.UserPreference.Notifications;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(string username)
        {
            var user = await _context.User
                .Include(u => u.UserPreference)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.User.AnyAsync(e => e.Username == username);
        }
    }
}
