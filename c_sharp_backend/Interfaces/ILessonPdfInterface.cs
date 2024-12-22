using c_sharp_backend.DTO;

namespace c_sharp_backend.Interfaces;

public interface ILessonPdfInterface
{
    Task<LessonPdfDto> GetPdfById(int id);
    Task<List<LessonPdfDto>> GetAllPdfs();
    Task<LessonPdfDto> AddPdf(string pdfName,LessonPdfDto lessonPdfDto);
    Task<LessonPdfDto> UpdatePdf(LessonPdfDto lessonPdfDto);
    Task<bool> DeletePdf(int id);
}