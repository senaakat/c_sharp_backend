using c_sharp_backend.DTO;
using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class LessonMapper
{
    public LessonDto MapLessonToLessonDto(Lesson lesson)
    {
        return new LessonDto
        {
            LessonName = lesson.LessonName,
            TeacherId = lesson.TeacherId,
        };
    }

    public Lesson MapLessonDtoToLesson(LessonDto lessonDto)
    {
        return new Lesson
        {
            LessonName = lessonDto.LessonName,
            TeacherId = lessonDto.TeacherId
        };
    }
}