using AutoMapper;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;


namespace DormitoryManagementSystem.Application.Services;


public class CheckInService : ICheckInService
{

    private readonly ICheckInRepository _repository;
    private readonly IMapper _mapper;


    public CheckInService(
        ICheckInRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }



    public async Task<CheckInDto> CheckInStudentAsync(CheckInDto dto)
    {

        var checkIn = _mapper.Map<CheckIn>(dto);


        checkIn.CheckInDate = DateTime.UtcNow;


        await _repository.AddAsync(checkIn);

        await _repository.SaveAsync();


        return _mapper.Map<CheckInDto>(checkIn);
    }




    public async Task<IEnumerable<CheckInDto>> GetCheckInHistoryAsync(int studentId)
    {

        var history =
            await _repository.GetHistoryAsync(studentId);


        return _mapper.Map<IEnumerable<CheckInDto>>(history);

    }

}