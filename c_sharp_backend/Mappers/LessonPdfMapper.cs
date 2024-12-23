using c_sharp_backend.DTO;

using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class LessonPdfMapper
{
    public LessonPdfDto MapPdfToPdfDto(LessonPdf lessonPdf)
    {
        return new LessonPdfDto
        {
            pdfName = lessonPdf.PdfName,
            lessonId = lessonPdf.LessonId,
            teacherId = lessonPdf.TeacherId
            
        };
    }

    public LessonPdf MapPdfDtoDtoToPdf(LessonPdfDto lessonDto)
    {
        return new LessonPdf
        {
            PdfName = lessonDto.pdfName,
            LessonId = lessonDto.lessonId,
            TeacherId = lessonDto.teacherId
            
        };
    }
}