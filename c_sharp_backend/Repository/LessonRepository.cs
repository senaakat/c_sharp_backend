using c_sharp_backend.Config;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;

public class LessonRepository
{
    private readonly AppDbContext _appDbContext;
    
    public LessonRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<Lesson> GetByLessonIdAsync(int id)
    {
        return await _appDbContext.lessons
            .Include(l => l.Teacher)
            .Include(l=>l.LessonPdfs)
            .FirstOrDefaultAsync(l => l.Id == id) ?? throw new InvalidOperationException();
    }
    
    public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
    {
        return await _appDbContext.lessons
            .Include(l => l.Teacher)
            .Include(l=>l.LessonPdfs)
            .ToListAsync();
    }
    
    public async Task<Lesson?> GetByLessonNameAsync(string lessonName)
    {
        return await _appDbContext.lessons
            .Include(l => l.Teacher)
            .Include(l => l.LessonPdfs)
            .FirstOrDefaultAsync(l => l.LessonName == lessonName);
    }
    
    public async Task<Lesson> AddLessonAsync(Lesson lesson)
    {
        try
        {
            await _appDbContext.lessons.AddAsync(lesson);
            await _appDbContext.SaveChangesAsync();
            return lesson;
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving the lesson to the database.", ex);
        }
    }

    public async Task UpdateAsync(Lesson lesson)
     {
         try{
             _appDbContext.lessons.Update(lesson);
             await _appDbContext.SaveChangesAsync();
         }
         catch(Exception ex){
             throw new Exception("Error updating the lesson to the database.", ex);
         }
     }
    
    public async Task DeleteAsync(int id)
    {
        var lesson = await GetByLessonIdAsync(id);
        if (lesson != null)
        {
            _appDbContext.lessons.Remove(lesson);
            await _appDbContext.SaveChangesAsync();
        }
    }
}