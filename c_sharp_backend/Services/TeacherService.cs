using c_sharp_backend.Config;
using c_sharp_backend.DTO;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Mappers;
using c_sharp_backend.Models;
using c_sharp_backend.Repository;
using c_sharp.Models;

namespace c_sharp_backend.Services;

public class TeacherService: ITeacherInterface
{
    private readonly TeacherRepository _teacherRepository;
    private readonly TeacherMapper _teacherMapper;
    private readonly AppDbContext _appDbContext;

    public TeacherService(AppDbContext appDbContext,TeacherRepository teacherRepository, TeacherMapper teacherMapper)
    {
        _teacherRepository = teacherRepository;
        _teacherMapper = teacherMapper;
        _appDbContext = appDbContext;
    }
    public async Task ChangeUserRoleToTeacherAsync(int userId)
    {
        var user = await _teacherRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        
        user.role = Role.TEACHER;
        
        var teacher = new Teacher
        {
            userId = userId,
            teacherGithub = null
        };
        
        await _teacherRepository.AddAsync(teacher);
        await _teacherRepository.UpdateAsync(user);
    }  
    
    public async Task<IEnumerable<TeacherDto>> GetAllTeachers()
    {
        var teachers = await _teacherRepository.GetAllAsync();
        return teachers.Select(TeacherMapper.MapTeacherToTeacherDto);
    }
    
    public async Task<TeacherDto?> GetTeacherId(int id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id);
        return teacher == null ? null : TeacherMapper.MapTeacherToTeacherDto(teacher);
    }

    public async Task<TeacherDto?> AddTeacher(TeacherDto teacherDTO)
    {
        var teacher = TeacherMapper.MapUserDtoTeacher(teacherDTO);
        await _teacherRepository.AddAsync(teacher);
        return TeacherMapper.MapTeacherToTeacherDto(teacher);
    }

    public async Task<TeacherDto?> UpdateTeacher(TeacherDto teacherDTO,int id)
    {
        var teacher = TeacherMapper.MapUserDtoTeacher(teacherDTO);
        teacher.id = id; // Güncellemelerde id atanır
        await _teacherRepository.UpdateAsync(teacher);
        return TeacherMapper.MapTeacherToTeacherDto(teacher);
    }

    public async Task DeleteTeacher(int id)
    {
        await _teacherRepository.DeleteAsync(id);
    }
}