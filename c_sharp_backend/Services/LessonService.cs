﻿using c_sharp_backend.DTO;
using c_sharp_backend.Interfaces;
using c_sharp_backend.Mappers;
using c_sharp_backend.Repository;

namespace c_sharp_backend.Services;

public class LessonService: ILessonInterface
{
    private readonly LessonRepository _lessonRepository;
    private readonly LessonMapper _lessonMapper;
    
    public LessonService(LessonRepository lessonRepository, LessonMapper lessonMapper)
    {
        _lessonRepository = lessonRepository;
        _lessonMapper = lessonMapper;
    }

    public async Task<LessonDto> GetLessonById(int id)
    {
        try
        {
            var lesson = await _lessonRepository.GetByLessonIdAsync(id);
            return _lessonMapper.MapLessonToLessonDto(lesson);
        }
        catch (Exception ex)
        {
            throw new Exception($" Lesson not found with id {id}.", ex);
        }
    }
    public async Task<LessonDto> GetLessonByName(string lessonName)
    {
        try
        {
            var lesson = await _lessonRepository.GetByLessonNameAsync(lessonName);
            return _lessonMapper.MapLessonToLessonDto(lesson);
        }
        catch (Exception ex)
        {
            throw new Exception($" Lesson not found with {lessonName}.", ex);
        }
    }
    
    public async Task<List<LessonDto>> GetAllLessons()
    {
        try
        {
            var lessons= await _lessonRepository.GetAllLessonsAsync();
            return lessons.Select(lesson => _lessonMapper.MapLessonToLessonDto(lesson)).ToList();
        }catch (Exception ex)
        {
            throw new Exception("All Lesson Not found", ex);
        }
    }
    public async Task<LessonDto> AddLesson(LessonDto lessonDto)
    {
        if (string.IsNullOrWhiteSpace(lessonDto.LessonName))
        {
            throw new Exception("Lesson name cannot be null or empty.");
        }
        try
        {
            var existinglesson = await _lessonRepository.GetByLessonNameAsync(lessonDto.LessonName);
            if (existinglesson != null)
            {
                throw new Exception($"Lesson with name {lessonDto.LessonName} already exists");
            }

            var lesson = _lessonMapper.MapLessonDtoToLesson(lessonDto);
            var createdLesson = await _lessonRepository.AddLessonAsync(lesson);
            return _lessonMapper.MapLessonToLessonDto(createdLesson);

        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding lesson with name {lessonDto.LessonName}: {ex.Message}", ex);
        }
    }
    public async Task<LessonDto> UpdateLesson(int id,LessonDto lessonDto)
    {
        if (string.IsNullOrWhiteSpace(lessonDto.LessonName))
        {
            throw new Exception("Lesson name cannot be null or empty.");
        }
        try
        {
            var lesson = await _lessonRepository.GetByLessonIdAsync(id);
            if (lesson == null)
            {
                throw new Exception($"Lesson not found");
            }
            
            lesson.LessonName = lessonDto.LessonName;
            lesson.TeacherId = lessonDto.TeacherId;
            
            await _lessonRepository.UpdateAsync(lesson);
            return _lessonMapper.MapLessonToLessonDto(lesson);
        }
        catch (Exception ex)
        {
            throw new Exception("Lesson not updated", ex);
        }
    }
    public async Task<bool> DeleteLesson(int id)
    {
        try
        {
            var lesson = await _lessonRepository.GetByLessonIdAsync(id);
            if (lesson == null)
            {
                throw new Exception($"Lesson with id {id} not found");
            }
            await _lessonRepository.DeleteAsync(id);
            return true;
             
        }catch (Exception ex) {
            throw new Exception("Lesson not deleted", ex);
        }
        
    }
}