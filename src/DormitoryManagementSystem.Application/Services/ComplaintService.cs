using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Services;

public class ComplaintService : IComplaintService
{
    private readonly IComplaintRepository _repository;

    public ComplaintService(IComplaintRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ComplaintDto>> GetAllAsync()
    {
        var complaints = await _repository.GetAllAsync();

        return complaints.Select(c => new ComplaintDto
        {
            ComplaintId = c.ComplaintId,
            SId = c.SId,
            Title = c.Title,
            Description = c.Description,
            Priority = c.Priority,
            Status = c.Status,
            ComplaintDate = c.ComplaintDate
        });
    }

    public async Task<ComplaintDto?> GetByIdAsync(int id)
    {
        var complaint = await _repository.GetByIdAsync(id);

        if (complaint == null)
            return null;

        return new ComplaintDto
        {
            ComplaintId = complaint.ComplaintId,
            SId = complaint.SId,
            Title = complaint.Title,
            Description = complaint.Description,
            Status = complaint.Status,
            ComplaintDate = complaint.ComplaintDate,
            Priority = complaint.Priority
        };
    }

    public async Task<ComplaintDto> CreateAsync(ComplaintDto dto)
    {
        var complaint = new Complaint
        {
            SId = dto.SId,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            ComplaintDate = dto.ComplaintDate,
            Priority = dto.Priority
        };

        await _repository.AddAsync(complaint);

        return dto;
    }

    public async Task<bool> UpdateAsync(int id, ComplaintDto dto)
    {
        var complaint = await _repository.GetByIdAsync(id);

        if (complaint == null)
            return false;

        complaint.SId = dto.SId;
        complaint.Title = dto.Title;
        complaint.Description = dto.Description;
        complaint.Priority = dto.Priority;
        complaint.Status = dto.Status;
        complaint.ComplaintDate = dto.ComplaintDate;

        await _repository.UpdateAsync(complaint);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var complaint = await _repository.GetByIdAsync(id);

        if (complaint == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}

