using System.Net.Http.Json;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class RegistrarService : IRegistrarService
{
    private readonly HttpClient _httpClient;

    public RegistrarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    

    public async Task<IEnumerable<RegistrarStudentDto>>
        GetStudentsAsync()
    {
        var students =
            await _httpClient.GetFromJsonAsync<
                IEnumerable<RegistrarStudentDto>
            >("api/students");

        return students ?? [];
    }

    public async Task<RegistrarStudentDto?>
        GetStudentByIdAsync(string studentId)
    {
        return await _httpClient.GetFromJsonAsync<
            RegistrarStudentDto
        >($"api/students/{studentId}");
    }

    public async Task<IEnumerable<RegistrarStudentDto>>
        SearchStudentsAsync(string query)
    {
        var students =
            await _httpClient.GetFromJsonAsync<
                IEnumerable<RegistrarStudentDto>
            >(
                $"api/students/search?query={Uri.EscapeDataString(query)}"
            );

        return students ?? [];
    }
    public async Task<IEnumerable<RegistrarDepartmentDto>>
    GetDepartmentsAsync()
{
    var departments =
        await _httpClient.GetFromJsonAsync<
            IEnumerable<RegistrarDepartmentDto>
        >("api/departments");

    return departments ?? [];
}
}