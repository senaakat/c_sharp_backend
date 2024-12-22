using c_sharp_backend.DTO;

using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class LessonPdfMapper
{
    public LessonPdfDto MapPdfToPdfDto(LessonPdf lessonPdf)
    {
        return new LessonPdfDto
        {
            pdfName = lessonPdf.pdfName,
            lessonId = lessonPdf.lessonId,
            teacherId = lessonPdf.teacherId
            
        };
    }

    public LessonPdf MapPdfDtoDtoToPdf(LessonPdfDto lessonDto)
    {
        return new LessonPdf
        {
            pdfName = lessonDto.pdfName,
            lessonId = lessonDto.lessonId,
            teacherId = lessonDto.teacherId
            
        };
    }
}