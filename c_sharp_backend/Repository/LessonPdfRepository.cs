using c_sharp_backend.Config;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Repository;

public class LessonPdfRepository
{
    private readonly AppDbContext _appDbContext;
    
    public LessonPdfRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<LessonPdf> GetByPdfIdAsync(int id)
    {
        return await _appDbContext.lessonPdfs
            .Include(l => l.pdfName)
            .Include(l=>l.lessonId)
            .Include(l => l.teacherId)
            .FirstOrDefaultAsync(l => l.id == id) ?? throw new InvalidOperationException();
    }
    
    
    public async Task<IEnumerable<LessonPdf>> GetAllPdfsAsync()
    {
        return await _appDbContext.lessonPdfs
            .Include(l => l.pdfName)
            .Include(l=>l.lessonId)
            .Include(l => l.teacherId)
            .ToListAsync();
    }
    
    public async Task<LessonPdf> GetByPdfNameAsync(string lessonName)
    {
        return await _appDbContext.lessonPdfs
            .Include(l => l.pdfName)
            .Include(l=>l.lessonId)
            .Include(l => l.teacherId)
            .FirstOrDefaultAsync(l => l.pdfName == lessonName) ?? throw new InvalidOperationException();
    }
    
    public async Task<LessonPdf> AddPdfAsync(LessonPdf lessonPdf)
    {
        await _appDbContext.lessonPdfs.AddAsync(lessonPdf);
        await _appDbContext.SaveChangesAsync();
        return lessonPdf;
    }

    public async Task UpdatePdfAsync(LessonPdf lessonPdf)
    {
        _appDbContext.lessonPdfs.Update(lessonPdf);
        await _appDbContext.SaveChangesAsync();
    }
    
    public async Task DeletPdfAsync(int id)
    {
        var lessonPdf = await _appDbContext.lessonPdfs.FindAsync(id);
        if (lessonPdf == null)
        {
            throw new InvalidOperationException();
        }
        _appDbContext.lessonPdfs.Remove(lessonPdf);
        await _appDbContext.SaveChangesAsync();
    }
}