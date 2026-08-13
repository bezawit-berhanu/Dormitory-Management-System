using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Enums;
using DormitoryManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagementSystem.Application.Services;

public class StaffAuthenticationService
    : IStaffAuthenticationService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IStaffRegistrarService _staffRegistrarService;
    private readonly IJwtService _jwtService;

    public StaffAuthenticationService(
        IUserRepository userRepository,
        IStaffRepository staffRepository,
        IStaffRegistrarService staffRegistrarService,
        IJwtService jwtService,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _staffRepository = staffRepository;
        _staffRegistrarService = staffRegistrarService;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }


    // ==========================================
    // REGISTER STAFF
    // ==========================================

    public async Task<AuthenticationResponseDto>
        RegisterAsync(RegisterStaffDto dto)
    {
        // --------------------------------------
        // 1. Check Registrar
        // --------------------------------------

        var registrarStaff =
            await _staffRegistrarService
                .GetStaffByEmployeeIdAsync(dto.EmployeeId);

        if (registrarStaff == null)
        {
            throw new Exception(
                "Staff member was not found in the Registrar system."
            );
        }


        // --------------------------------------
        // 2. Verify Full Name
        // --------------------------------------

        if (!string.Equals(
                registrarStaff.FullName.Trim(),
                dto.FullName.Trim(),
                StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                "Employee ID and full name do not match Registrar records."
            );
        }


        // --------------------------------------
        // 3. Check Registrar Status
        // --------------------------------------

        if (registrarStaff.Status != 1)
        {
            throw new Exception(
                "This staff member is not active in the Registrar system."
            );
        }


        // --------------------------------------
        // 4. Check existing staff account
        // --------------------------------------

        var existingStaff =
            await _staffRepository
                .GetByEmployeeIdAsync(dto.EmployeeId);

        if (existingStaff != null)
        {
            throw new Exception(
                "An account already exists for this staff member."
            );
        }


        // --------------------------------------
        // 5. Check email
        // --------------------------------------

        var existingUser =
            await _userRepository
                .GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new Exception(
                "This email is already registered."
            );
        }


        // --------------------------------------
        // 6. Validate password
        // --------------------------------------

        if (dto.Password != dto.ConfirmPassword)
        {
            throw new Exception(
                "Passwords do not match."
            );
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new Exception(
                "Password is required."
            );
        }


        // --------------------------------------
        // 7. Create User
        // --------------------------------------

        var user = new User
        {
            FullName = registrarStaff.FullName,

            Email = dto.Email,

            PhoneNumber = registrarStaff.PhoneNumber,

            // Existing Staff role from DbSeeder.
            RoleId = 2,

            Status = UserStatus.Active,

            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                dto.Password
            );

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();


        // --------------------------------------
        // 8. Create Staff record
        // --------------------------------------

        var staff = new Staff
        {
            UserId = user.UserId,

            EmployeeId = registrarStaff.EmployeeId,

            Campus = registrarStaff.Campus,

            Role = registrarStaff.Role,

            AssignedBlock = registrarStaff.AssignedBlock,

            Status = (UserStatus)registrarStaff.Status
        };

        await _staffRepository.AddAsync(staff);

        await _staffRepository.SaveChangesAsync();


        // --------------------------------------
        // 9. Generate JWT
        // --------------------------------------

        var role = "Staff";

        var token =
            _jwtService.GenerateToken(
                user.UserId,
                staff.EmployeeId,
                role
            );


        // --------------------------------------
        // 10. Return response
        // --------------------------------------

        return new AuthenticationResponseDto
        {
            Token = token,

            User = MapToDto(
                user,
                staff,
                role
            )
        };
    }


    // ==========================================
    // STAFF LOGIN
    // ==========================================

    public async Task<AuthenticationResponseDto>
        LoginAsync(StaffLoginDto dto)
    {
        // --------------------------------------
        // 1. Find User
        // --------------------------------------

        var user =
            await _userRepository
                .GetByEmailAsync(dto.Identifier);

        if (user == null)
        {
            throw new Exception(
                "Invalid staff email or password."
            );
        }


        // --------------------------------------
        // 2. Find Staff
        // --------------------------------------
var staff =
    await _staffRepository
        .GetByUserIdAsync(user.UserId);

        if (staff == null)
        {
            throw new Exception(
                "Staff account was not found."
            );
        }


        // --------------------------------------
        // 3. Check password
        // --------------------------------------

        var passwordResult =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password
            );

        if (passwordResult ==
            PasswordVerificationResult.Failed)
        {
            throw new Exception(
                "Invalid staff email or password."
            );
        }


        // --------------------------------------
        // 4. Check active status
        // --------------------------------------

        if (user.Status != UserStatus.Active)
        {
            throw new Exception(
                "This staff account is inactive."
            );
        }


        // --------------------------------------
        // 5. Generate JWT
        // --------------------------------------

        var role = "Staff";

        var token =
            _jwtService.GenerateToken(
                user.UserId,
                staff.EmployeeId,
                role
            );


        // --------------------------------------
        // 6. Return response
        // --------------------------------------

        return new AuthenticationResponseDto
        {
            Token = token,

            User = MapToDto(
                user,
                staff,
                role
            )
        };
    }


    // ==========================================
    // MAP STAFF TO DTO
    // ==========================================

    private static UserDto MapToDto(
        User user,
        Staff staff,
        string role)
    {
        return new UserDto
        {
            UserId = user.UserId,

            FullName = user.FullName,

            Email = user.Email,

            PhoneNumber =
                user.PhoneNumber ?? string.Empty,

            Role = role,

            Department = string.Empty,

            IsActive =
                user.Status == UserStatus.Active
        };
    }
}