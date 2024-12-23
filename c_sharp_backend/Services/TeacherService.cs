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
    private readonly UserRepository _userRepository;
    private readonly TeacherMapper _teacherMapper;
    private readonly AppDbContext _appDbContext;

    public TeacherService(AppDbContext appDbContext,TeacherRepository teacherRepository, TeacherMapper teacherMapper, UserRepository userRepository)
    {
        _teacherRepository = teacherRepository;
        _userRepository = userRepository;
        _teacherMapper = teacherMapper;
        _appDbContext = appDbContext;
    }
    public async Task ChangeUserRoleToTeacherAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        
        user.Role = Role.Teacher;
        
        var teacher = new Teacher
        {
            UserId = userId,
            TeacherGithub = null
        };
        
        await _teacherRepository.AddAsync(teacher);
        await _userRepository.UpdateUserAsync(user);
    }  
    
    public async Task<IEnumerable<TeacherDto>> GetAllTeachers()
    {
        var teachers = await _teacherRepository.GetAllAsync();
        return teachers.Select(_teacherMapper.MapTeacherToTeacherDto);
    }
    
    public async Task<TeacherDto?> GetTeacherId(int id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id);
        return teacher == null ? null : _teacherMapper.MapTeacherToTeacherDto(teacher);
    }

    public async Task<TeacherDto?> AddTeacher(int userId,TeacherDto teacherDTO)
    {
        
        var teacher = _teacherMapper.MapUserDtoTeacher(teacherDTO);
        teacher.UserId = userId;
        teacher.TeacherGithub=teacherDTO.TeacherGithub;
        await _teacherRepository.AddAsync(teacher);
        return _teacherMapper.MapTeacherToTeacherDto(teacher);
    }

    public async Task<TeacherDto?> UpdateTeacher(TeacherDto teacherDTO,int id)
    {
        var teacher = _teacherMapper.MapUserDtoTeacher(teacherDTO);
        teacher.Id = id;
        await _teacherRepository.UpdateAsync(teacher);
        return _teacherMapper.MapTeacherToTeacherDto(teacher);
    }

    public async Task DeleteTeacher(int id)
    {
        await _teacherRepository.DeleteAsync(id);
    }
}