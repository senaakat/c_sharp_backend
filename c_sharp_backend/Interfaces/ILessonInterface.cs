using c_sharp_backend.DTO;

namespace c_sharp_backend.Interfaces;

public interface ILessonInterface
{
    Task<LessonDto> GetLessonById(int id);
    Task<List<LessonDto>> GetAllLessons();
    Task<LessonDto> AddLesson(LessonDto lessonDto);
    Task<LessonDto> UpdateLesson(LessonDto lessonDto);
    Task<bool> DeleteLesson(int id);
}