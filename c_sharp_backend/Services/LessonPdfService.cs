using c_sharp_backend.DTO;
using c_sharp_backend.Mappers;
using c_sharp_backend.Repository;

namespace c_sharp_backend.Services;

public class LessonPdfService
{
    private readonly LessonPdfRepository _lessonPdfRepository;
    private readonly LessonPdfMapper _lessonPdfMapper;
    
    public LessonPdfService(LessonPdfRepository lessonPdfRepository, LessonPdfMapper lessonPdfMapper)
    {
        _lessonPdfRepository = lessonPdfRepository;
        _lessonPdfMapper = lessonPdfMapper;
    }
    
    public async Task<LessonPdfDto> GetPdfById(int id)
    {
        try
        {
            var lessonPdf = await _lessonPdfRepository.GetByPdfIdAsync(id);
            return _lessonPdfMapper.MapPdfToPdfDto(lessonPdf);
        }
        catch (Exception ex)
        {
            throw new Exception($" Pdf not found with id {id}.", ex);
        }
    }
    public async Task<List<LessonPdfDto>> GetAllPdfs()
    {
        try
        {
            var lessonPdfs = await _lessonPdfRepository.GetAllPdfsAsync();
            return lessonPdfs.Select(lessonPdf => _lessonPdfMapper.MapPdfToPdfDto(lessonPdf)).ToList();
        }catch (Exception ex)
        {
            throw new Exception("Pdfs Not found", ex);
        }
    }
    public async Task<LessonPdfDto> AddPdf(string pdfName,LessonPdfDto lessonPdfDto)
    {
        try
        {
            var existinglessonPdf = _lessonPdfRepository.GetByPdfNameAsync(pdfName);
            if (existinglessonPdf != null)
            {
                throw new Exception($"Pdf with name {pdfName} already exists");
            }

            var lessonPdf = _lessonPdfMapper.MapPdfDtoDtoToPdf(lessonPdfDto);
            var createdLessonPdf = await _lessonPdfRepository.AddPdfAsync(lessonPdf);
            return _lessonPdfMapper.MapPdfToPdfDto(createdLessonPdf);

        }
        catch (Exception ex)
        {
            throw new Exception("Pdf not added", ex);
        }
    }
    public async Task<LessonPdfDto> UpdatePdf(LessonPdfDto lessonPdfDto)
    {
        try
        {
            var lessonPdf = await _lessonPdfRepository.GetByPdfNameAsync(lessonPdfDto.pdfName!);
            if (lessonPdf == null)
            {
                throw new Exception($"Pdf not found");
            }

            lessonPdf = _lessonPdfMapper.MapPdfDtoDtoToPdf(lessonPdfDto);
            await _lessonPdfRepository.UpdatePdfAsync(lessonPdf);
            return _lessonPdfMapper.MapPdfToPdfDto(lessonPdf);
        }
        catch (Exception ex)
        {
            throw new Exception("Pdf not updated", ex);
        }
    }
    public async Task<bool> DeletePdf(int id)
    {
        try
        {
            var lessonPdf = await _lessonPdfRepository.GetByPdfIdAsync(id);
            if (lessonPdf == null)
            {
                throw new Exception($"Pdf with id {id} not found");
            }
            await _lessonPdfRepository.DeletPdfAsync(id);
            return true;
             
        }catch (Exception ex) {
            throw new Exception("Pdf not deleted", ex);
        }
    }
}