using c_sharp_backend.DTO;

namespace c_sharp_backend.Interfaces;

public interface ITeacherInterface
{ 
    Task ChangeUserRoleToTeacherAsync(int userId);
    Task<IEnumerable<TeacherDto>> GetAllTeachers();
    Task<TeacherDto?> GetTeacherId(int id);
    Task<TeacherDto?> AddTeacher(TeacherDto userDto);
    
    Task<TeacherDto?> UpdateTeacher(TeacherDto userDto, int id);

    Task DeleteTeacher(int id);
}