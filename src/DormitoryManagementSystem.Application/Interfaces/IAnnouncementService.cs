using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementDto>> GetAllAsync();

    Task<AnnouncementDto?> GetByIdAsync(int id);

    Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsAsync();

    Task<AnnouncementDto> CreateAsync(AnnouncementDto dto);

    Task<bool> UpdateAsync(int id, AnnouncementDto dto);

    Task<bool> DeleteAsync(int id);
}