using AutoMapper;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public StudentService(
        IStudentRepository studentRepository,
        IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }


    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
            return null;

        return _mapper.Map<StudentDto>(student);
    }


    public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
    {
        var student = _mapper.Map<Student>(dto);

        await _studentRepository.AddAsync(student);

        return _mapper.Map<StudentDto>(student);
    }


    public async Task<bool> UpdateStudentAsync(
        int id,
        UpdateStudentDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
            return false;

        _mapper.Map(dto, student);

        await _studentRepository.UpdateAsync(student);

        return true;
    }


    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
            return false;

        await _studentRepository.DeleteAsync(student);

        return true;
    }
}