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

   
}