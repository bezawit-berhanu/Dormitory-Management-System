using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
namespace DormitoryManagementSystem.Application.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _repository;

    public AnnouncementService(IAnnouncementRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
    {
        var announcements = await _repository.GetAllAsync();

        return announcements.Select(a => new AnnouncementDto
        {
            AnnouncementId = a.AnnouncementId,
            CreatedBy = a.CreatedBy,
            Title = a.Title,
            Message = a.Message,
            PublishedDate = a.PublishedDate,
            ExpiryDate = a.ExpiryDate,
            Status = a.Status
        });
    }

    public async Task<AnnouncementDto?> GetByIdAsync(int id)
    {
        var announcement = await _repository.GetByIdAsync(id);

        if (announcement == null)
            return null;

        return new AnnouncementDto
        {
            AnnouncementId = announcement.AnnouncementId,
            CreatedBy = announcement.CreatedBy,
            Title = announcement.Title,
            Message = announcement.Message,
            PublishedDate = announcement.PublishedDate,
            ExpiryDate = announcement.ExpiryDate,
            Status = announcement.Status
        };
    }

    public async Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsAsync()
    {
        var announcements = await _repository.GetAllAsync();

        var active = announcements.Where(a =>
            a.Status == "Active" &&
            a.ExpiryDate >= DateTime.Now);

        return active.Select(a => new AnnouncementDto
        {
            AnnouncementId = a.AnnouncementId,
            CreatedBy = a.CreatedBy,
            Title = a.Title,
            Message = a.Message,
            PublishedDate = a.PublishedDate,
            ExpiryDate = a.ExpiryDate,
            Status = a.Status
        });
    }

    public async Task<AnnouncementDto> CreateAsync(AnnouncementDto dto)
    {
        var announcement = new Announcement
        {
            CreatedBy = dto.CreatedBy,
            Title = dto.Title,
            Message = dto.Message,
            PublishedDate = dto.PublishedDate,
            ExpiryDate = dto.ExpiryDate,
            Status = dto.Status
        };

        var created = await _repository.AddAsync(announcement);

        return new AnnouncementDto
        {
            AnnouncementId = created.AnnouncementId,
            CreatedBy = created.CreatedBy,
            Title = created.Title,
            Message = created.Message,
            PublishedDate = created.PublishedDate,
            ExpiryDate = created.ExpiryDate,
            Status = created.Status
        };
    }

    public async Task<bool> UpdateAsync(int id, AnnouncementDto dto)
    {
        var announcement = await _repository.GetByIdAsync(id);

        if (announcement == null)
            return false;

        announcement.CreatedBy = dto.CreatedBy;
        announcement.Title = dto.Title;
        announcement.Message = dto.Message;
        announcement.PublishedDate = dto.PublishedDate;
        announcement.ExpiryDate = dto.ExpiryDate;
        announcement.Status = dto.Status;

        await _repository.UpdateAsync(announcement);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var announcement = await _repository.GetByIdAsync(id);

        if (announcement == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}