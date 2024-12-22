using c_sharp_backend.DTO;
using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class TeacherMapper
{
    public TeacherDto MapTeacherToTeacherDto(Teacher teacher)
    {
        return new TeacherDto
        {
            TeacherGithub = teacher.teacherGithub,
            UserId = teacher.userId
        };
    }

    public Teacher MapUserDtoTeacher(TeacherDto teacherDto)
    {
        return new Teacher
        {
            teacherGithub = teacherDto.TeacherGithub,
            userId = teacherDto.UserId
        };
    }
}