using c_sharp_backend.DTO;
using c_sharp.Models;

namespace c_sharp_backend.Mappers;

public class TeacherMapper
{
    public TeacherDto MapTeacherToTeacherDto(Teacher teacher)
    {
        return new TeacherDto
        {
            TeacherGithub = teacher.TeacherGithub,
            UserId = teacher.UserId
        };
    }

    public Teacher MapUserDtoTeacher(TeacherDto teacherDto)
    {
        return new Teacher
        {
            TeacherGithub = teacherDto.TeacherGithub,
            UserId = teacherDto.UserId
        };
    }
}