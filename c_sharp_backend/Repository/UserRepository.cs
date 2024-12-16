using c_sharp_backend.Interfaces;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;
public class UserRepository
{
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _dbContext.Set<User>().ToListAsync();

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.id == id);
        public async Task<User?> GetUserByEmailAsync(string email) =>
            await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.email == email);

        public async Task<User> AddUserAsync(User user)
        {
            await _dbContext.Set<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _dbContext.Set<User>().Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Set<User>().FindAsync(id);
            if (user != null)
            {
                _dbContext.Set<User>().Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
}