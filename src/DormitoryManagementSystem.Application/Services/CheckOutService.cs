using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using AutoMapper;


namespace DormitoryManagementSystem.Application.Services;

public class CheckOutService : ICheckOutService
{
   private readonly ICheckOutRepository _repository;
private readonly IMapper _mapper;


public CheckOutService(
    ICheckOutRepository repository,
    IMapper mapper)
{
    _repository = repository;
    _mapper = mapper;
}

    public async Task<IEnumerable<CheckOutDto>> GetCheckOutHistoryAsync(int studentId)
    {
        var checkOuts = await _repository.GetByStudentIdAsync(studentId);

        return checkOuts.Select(x => new CheckOutDto
        {
            CheckOutId = x.CheckOutId,
            RoomAssignmentId = x.RoomAssignmentId,
            CheckOutDate = x.CheckOutDate,
            Reason = x.Reason
        });
    }

    public async Task<CheckOutDto?> GetCheckOutByIdAsync(int id)
    {
        var checkOut = await _repository.GetByIdAsync(id);

        if (checkOut == null)
            return null;

        return new CheckOutDto
        {
            CheckOutId = checkOut.CheckOutId,
            RoomAssignmentId = checkOut.RoomAssignmentId,
            CheckOutDate = checkOut.CheckOutDate,
            Reason = checkOut.Reason
        };
    }

    public async Task<CheckOutDto> CheckOutStudentAsync(CheckOutDto dto)
    {
        var checkOut = new CheckOut
        {
            RoomAssignmentId = dto.RoomAssignmentId,
            CheckOutDate = dto.CheckOutDate,
            Reason = dto.Reason
        };

        var result = await _repository.AddAsync(checkOut);

        return new CheckOutDto
        {
            CheckOutId = result.CheckOutId,
            RoomAssignmentId = result.RoomAssignmentId,
            CheckOutDate = result.CheckOutDate,
            Reason = result.Reason
        };
    }

    public async Task<bool> UpdateCheckOutAsync(int id, CheckOutDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            return false;

        existing.RoomAssignmentId = dto.RoomAssignmentId;
        existing.CheckOutDate = dto.CheckOutDate;
        existing.Reason = dto.Reason;

        await _repository.UpdateAsync(existing);

        return true;
    }

    public async Task<bool> DeleteCheckOutAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}