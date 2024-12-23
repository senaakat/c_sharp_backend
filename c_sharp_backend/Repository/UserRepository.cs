using c_sharp_backend.Config;
using c_sharp_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;
public class UserRepository
{
    private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _appDbContext.Set<User>().ToListAsync();
        
        public async Task<User?> GetUserByEmail(String email) =>
            await _appDbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _appDbContext.Set<User>().FirstOrDefaultAsync(u => u.id== id);
        public async Task<User?> GetUserByEmailAsync(string email) =>
            await _appDbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> AddUserAsync(User user)
        {
            await _appDbContext.Set<User>().AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _appDbContext.Set<User>().Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _appDbContext.Set<User>().FindAsync(id);
            if (user != null)
            {
                _appDbContext.Set<User>().Remove(user);
                await _appDbContext.SaveChangesAsync();
            }
        }
}