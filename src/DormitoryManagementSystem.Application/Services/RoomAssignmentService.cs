using AutoMapper;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class RoomAssignmentService : IRoomAssignmentService
{
    private readonly IRoomAssignmentRepository _roomAssignmentRepository;
    private readonly IMapper _mapper;

    public RoomAssignmentService(
        IRoomAssignmentRepository roomAssignmentRepository,
        IMapper mapper)
    {
        _roomAssignmentRepository = roomAssignmentRepository;
        _mapper = mapper;
    }

    public async Task<RoomAssignmentDto?>
        GetAssignmentByIdAsync(int id)
    {
        var assignment =
            await _roomAssignmentRepository.GetByIdAsync(id);

        if (assignment == null)
            return null;

        return _mapper.Map<RoomAssignmentDto>(assignment);
    }



    public async Task<IEnumerable<RoomAssignmentDto>>
        GetStudentAssignmentsAsync(int sId)
    {
        var assignments =
            await _roomAssignmentRepository
                .GetByStudentIdAsync(sId);

        return _mapper.Map<
            IEnumerable<RoomAssignmentDto>
        >(assignments);
    }

   

    public async Task<RoomAssignmentDto>
        AssignRoomAsync(RoomAssignmentDto dto)
    {
        var assignment =
            _mapper.Map<RoomAssignment>(dto);

        await _roomAssignmentRepository
            .AddAsync(assignment);

        return _mapper.Map<RoomAssignmentDto>(
            assignment);
    }


    public async Task<bool>
        UpdateAssignmentAsync(
            int id,
            RoomAssignmentDto dto)
    {
        var assignment =
            await _roomAssignmentRepository
                .GetByIdAsync(id);

        if (assignment == null)
            return false;

        _mapper.Map(dto, assignment);

        await _roomAssignmentRepository
            .UpdateAsync(assignment);

        return true;
    }


    public async Task<bool>
        DeleteAssignmentAsync(int id)
    {
        var assignment =
            await _roomAssignmentRepository
                .GetByIdAsync(id);

        if (assignment == null)
            return false;

        await _roomAssignmentRepository
            .DeleteAsync(assignment);

        return true;
    }
}