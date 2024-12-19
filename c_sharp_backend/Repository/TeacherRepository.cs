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

    // Öğretmeni ID'ye göre getir
    public async Task<Teacher> GetByIdAsync(int id)
    {
        return await _appDbContext.teachers
            .Include(t => t.user) 
            .Include(t => t.lessons)
            .Include(t => t.lessonPdfs) 
            .FirstOrDefaultAsync(t => t.id == id);
    }

    // Tüm öğretmenleri getir
    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _appDbContext.teachers
            .Include(t => t.user) // User ile ilişkiyi dahil et
            .ToListAsync();
    }

    // Yeni öğretmen ekle
    public async Task AddAsync(Teacher teacher)
    {
        await _appDbContext.teachers.AddAsync(teacher);
        await _appDbContext.SaveChangesAsync();
    }

    // Öğretmeni güncelle
    public async Task UpdateAsync(Teacher teacher)
    {
        _appDbContext.teachers.Update(teacher);
        await _appDbContext.SaveChangesAsync();
    }

    // Öğretmeni sil
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