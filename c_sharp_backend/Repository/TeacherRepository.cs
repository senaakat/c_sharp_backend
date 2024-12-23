using c_sharp_backend.Config;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;

public class TeacherRepository
{
    private readonly AppDbContext _appDbContext;

    public TeacherRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<Teacher> GetByIdAsync(int id)
    {
        return await _appDbContext.teachers
            .Include(t => t.User) 
            .Include(t => t.Lessons)
            .Include(t => t.LessonPdfs) 
            .FirstOrDefaultAsync(t => t.Id == id) ?? throw new InvalidOperationException();
    }
    
    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _appDbContext.teachers
            .Include(t => t.User)
            .ToListAsync();
    }
    
    public async Task AddAsync(Teacher teacher)
    {
        await _appDbContext.teachers.AddAsync(teacher);
        await _appDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Teacher teacher)
    {
        _appDbContext.teachers.Update(teacher);
        await _appDbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var teacher = await GetByIdAsync(id);
        if (teacher != null)
        {
            _appDbContext.teachers.Remove(teacher);
            await _appDbContext.SaveChangesAsync();
        }
    }
}