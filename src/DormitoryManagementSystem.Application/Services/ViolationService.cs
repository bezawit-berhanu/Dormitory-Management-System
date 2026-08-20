using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class ViolationService : IViolationService
{
    private readonly IViolationRepository _violationRepository;
    public ViolationService(IViolationRepository violationRepository)
    {
        _violationRepository = violationRepository;
    }
    public async Task<IEnumerable<ViolationDto>> GetAllAsync()
    {
        return new List<ViolationDto>();
    }

    public async Task<ViolationDto?> GetByIdAsync(int id)
    {
        return null;
    }

    public async Task<ViolationDto> CreateAsync(ViolationDto dto)
    {
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, ViolationDto dto)
    {
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return true;
    }
}