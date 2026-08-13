using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IStaffRegistrarService
{
    Task<RegistrarStaffDto?> GetStaffByEmployeeIdAsync(
        string employeeId);
}