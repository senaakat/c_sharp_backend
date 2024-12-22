using c_sharp_backend.DTO;
using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class LessonMapper
{
    public LessonDto MapLessonToLessonDto(Lesson lesson)
    {
        return new LessonDto
        {
            LessonName = lesson.lessonName,
            TeacherId = lesson.teacherId,
        };
    }

    public Lesson MapLessonDtoToLesson(LessonDto lessonDto)
    {
        return new Lesson
        {
            lessonName = lessonDto.LessonName,
            teacherId = lessonDto.TeacherId
        };
    }
}