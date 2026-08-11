using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
namespace DormitoryManagementSystem.Application.Services;

public class InspectionService : IInspectionService
{
    private readonly IInspectionRepository _repository;

    public InspectionService(IInspectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<InspectionDto>> GetAllAsync()
    {
        var inspections = await _repository.GetAllAsync();

        return inspections.Select(i => new InspectionDto
        {
            InspectionId = i.InspectionId,
            RoomId = i.RoomId,
            InspectionDate = i.InspectionDate,
            InspectedByUserId = i.InspectedByUserId,
            Remarks = i.Remarks,
            Status = i.Status
        });
    }

    public async Task<InspectionDto?> GetByIdAsync(int id)
    {
        var inspection = await _repository.GetByIdAsync(id);

        if (inspection == null)
            return null;

        return new InspectionDto
        {
            InspectionId = inspection.InspectionId,
            RoomId = inspection.RoomId,
            InspectionDate = inspection.InspectionDate,
            InspectedByUserId = inspection.InspectedByUserId,
            Remarks = inspection.Remarks,
        };
    }

    public async Task<InspectionDto> CreateAsync(InspectionDto dto)
    {
        var inspection = new Inspection
        {
            RoomId = dto.RoomId,
            InspectionDate = dto.InspectionDate,
            InspectedByUserId = dto.InspectedByUserId,
            Remarks = dto.Remarks,
            Status = dto.Status
        };

        await _repository.AddAsync(inspection);

        dto.InspectionId = inspection.InspectionId;


        return dto;
    }

    public async Task<bool> UpdateAsync(int id, InspectionDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            return false;

        existing.RoomId = dto.RoomId;
        existing.InspectionDate = dto.InspectionDate;
        existing.InspectedByUserId = dto.InspectedByUserId;
        existing.Remarks = dto.Remarks;
        existing.Status = dto.Status;

        await _repository.UpdateAsync(existing);

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}