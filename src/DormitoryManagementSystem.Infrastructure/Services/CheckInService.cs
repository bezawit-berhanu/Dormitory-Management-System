using AutoMapper;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Application.Services;

public class CheckInService : ICheckInService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public CheckInService(
        ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<CheckInDto> CheckInStudentAsync(CheckInDto dto)
    {
        var checkIn = _mapper.Map<CheckIn>(dto);

        checkIn.CheckInDate = DateTime.UtcNow;

        await _context.CheckIns.AddAsync(checkIn);

        await _context.SaveChangesAsync();

        return _mapper.Map<CheckInDto>(checkIn);
    }


    public async Task<IEnumerable<CheckInDto>> GetCheckInHistoryAsync(int studentId)
    {
        var history = await _context.CheckIns
            .Where(c => c.SId == studentId)
            .ToListAsync();


        return _mapper.Map<IEnumerable<CheckInDto>>(history);
    }
}