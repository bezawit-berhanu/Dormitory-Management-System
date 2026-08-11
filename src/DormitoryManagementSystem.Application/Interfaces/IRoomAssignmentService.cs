using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IRoomAssignmentService
{
    Task<IEnumerable<RoomAssignmentDto>>
        GetStudentAssignmentsAsync(int sId);

    
    Task<RoomAssignmentDto?>
        GetAssignmentByIdAsync(int id);

   
    Task<RoomAssignmentDto>
        AssignRoomAsync(RoomAssignmentDto dto);

    Task<bool>
        UpdateAssignmentAsync(
            int id,
            RoomAssignmentDto dto);

    Task<bool>
        DeleteAssignmentAsync(int id);
}