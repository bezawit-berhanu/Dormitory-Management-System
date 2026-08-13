using System.Net.Http.Json;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class StaffRegistrarService : IStaffRegistrarService
{
    private readonly HttpClient _httpClient;

    public StaffRegistrarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RegistrarStaffDto?>
        GetStaffByEmployeeIdAsync(string employeeId)
    {
        return await _httpClient.GetFromJsonAsync<
            RegistrarStaffDto
        >($"api/staff/{employeeId}");
    }
}