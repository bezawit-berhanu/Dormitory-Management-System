using AutoMapper;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Mapping;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Student, StudentDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<RoomAssignment, RoomAssignmentDto>().ReverseMap();

        CreateMap<CheckIn, CheckInDto>().ReverseMap();

        CreateMap<CheckOut, CheckOutDto>().ReverseMap();

        CreateMap<QRCode, QRCodeDto>().ReverseMap();
        CreateMap<Student, StudentDto>();
            }
}