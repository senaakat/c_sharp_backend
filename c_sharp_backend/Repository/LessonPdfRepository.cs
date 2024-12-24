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
            .FirstOrDefaultAsync(l => l.Id == id) ?? throw new InvalidOperationException();
    }
    
    
    public async Task<IEnumerable<LessonPdf>> GetAllPdfsAsync()
    {
        return await _appDbContext.lessonPdfs.ToListAsync();
    }
    
    public async Task<LessonPdf?> GetByPdfNameAsync(string pdfName)
    {
        return await _appDbContext.lessonPdfs
            .FirstOrDefaultAsync(l => l.PdfName == pdfName);
    }
    
    public async Task<LessonPdf> AddPdfAsync(LessonPdf lessonPdf)
    {
        try
        {
            await _appDbContext.lessonPdfs.AddAsync(lessonPdf);
            await _appDbContext.SaveChangesAsync();
            return lessonPdf;
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving the pdf to the database.", ex);
        }
    }

    public async Task UpdatePdfAsync(LessonPdf lessonPdf)
    {
        try{
            _appDbContext.lessonPdfs.Update(lessonPdf);
            await _appDbContext.SaveChangesAsync();
        }
        catch(Exception ex){
            throw new Exception("Error updating the pdf to the database.", ex);
        }
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