using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Enums;
using DormitoryManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using DormitoryManagementSystem.Application.Validators;

namespace DormitoryManagementSystem.Application.Services;

public class AuthenticationService
    : IAuthenticationService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IRegistrarService _registrarService;
    private readonly IJwtService _jwtService;
    private readonly IDepartmentRepository _departmentRepository;

    public AuthenticationService(
        IUserRepository userRepository,
        IStudentRepository studentRepository,
        IRegistrarService registrarService,
        IDepartmentRepository departmentRepository,
        IJwtService jwtService,  
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _registrarService = registrarService;
        _jwtService = jwtService;
         _passwordHasher = passwordHasher;
         _departmentRepository = departmentRepository;
    }


    

    public async Task<AuthenticationResponseDto>
        RegisterAsync(RegisterDto dto)
    {
        AuthenticationInputValidator.ValidateRegistration(dto.Email, dto.PhoneNumber, dto.Password, dto.ConfirmPassword);
        dto.Email = dto.Email.Trim();
        dto.PhoneNumber = AuthenticationInputValidator.NormalizePhoneNumber(dto.PhoneNumber);

      

        var registrarStudent =
            await _registrarService
                .GetStudentByIdAsync(dto.StudentId);

        if (registrarStudent == null)
        {
            throw new Exception(
                "Student was not found in the Registrar system."
            );
        }


     

        if (!string.Equals(
                registrarStudent.FullName.Trim(),
                dto.FullName.Trim(),
                StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                "Student ID and full name do not match Registrar records."
            );
        }


     

        var existingStudent =
            await _studentRepository
                .GetByStudentIdAsync(dto.StudentId);

        if (existingStudent != null)
        {
            throw new Exception(
                "An account already exists for this student."
            );
        }


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
        // 5. Create User
        // --------------------------------------

        var user = new User
        {
            FullName = registrarStudent.FullName,

            Email = dto.Email,

            PhoneNumber = dto.PhoneNumber,

            RoleId = 3,

            Status = UserStatus.Active,

            CreatedAt = DateTime.UtcNow
        };
           user.PasswordHash = _passwordHasher.HashPassword(
    user,
    dto.Password
);


        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

var department = await _departmentRepository
    .GetByRegistrarIdAsync(registrarStudent.DepartmentId);

if (department == null)
{
    department = new Department
    {
        RegistrarDepartmentId = registrarStudent.DepartmentId,
        DepartmentName = registrarStudent.Department
    };

    await _departmentRepository.AddAsync(department);
    await _departmentRepository.SaveChangesAsync();
}
        // --------------------------------------
        // 6. Create Student record
        // --------------------------------------

      var student = new Student
{
    UserId = user.UserId,
    StudentId = registrarStudent.StudentId,

    DepartmentId = department.DepartmentId,

    Gender = registrarStudent.Gender,
    DateOfBirth = registrarStudent.DateOfBirth,
    YearOfStudy = registrarStudent.YearOfStudy,
    Status = (UserStatus)registrarStudent.Status
};

        await _studentRepository.AddAsync(student);
         await _studentRepository.SaveChangesAsync();

        // --------------------------------------
        // 7. Generate JWT
        // --------------------------------------

        var role =
            "Student";

        var token =
            _jwtService.GenerateToken(
                user.UserId,
                student.StudentId,
                role
            );


        // --------------------------------------
        // 8. Return response
        // --------------------------------------

        return new AuthenticationResponseDto
        {
            Token = token,

            User = MapToDto(
                user,
                role
            )
        };
    }


    // ==========================================
    // LOGIN
    // ==========================================

    public async Task<AuthenticationResponseDto>
        LoginAsync(LoginDto dto)
    {
        User? user = null;

        Student? student = null;


        // --------------------------------------
        // STUDENT LOGIN
        // --------------------------------------

        if (!dto.Identifier.Contains("@"))
        {
            student =
                await _studentRepository
                    .GetByStudentIdAsync(
                        dto.Identifier
                    );

            if (student == null)
            {
                throw new Exception(
                    "Invalid Student ID or password."
                );
            }


            user =
                await _userRepository
                    .GetByIdAsync(student.UserId);
        }


        // --------------------------------------
        // STAFF LOGIN
        // --------------------------------------

        else
        {
            user =
                await _userRepository
                    .GetByEmailAsync(
                        dto.Identifier
                    );
        }


        if (user == null)
        {
            throw new Exception(
                "Invalid Student ID/email or password."
            );
        }


        // --------------------------------------
        // Password
        // --------------------------------------

        var passwordResult =
    _passwordHasher.VerifyHashedPassword(
        user,
        user.PasswordHash,
        dto.Password
    );

if (passwordResult == PasswordVerificationResult.Failed)
{
    throw new Exception(
        "Invalid Student ID/email or password."
    );
}

if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
{
    user.PasswordHash =
        _passwordHasher.HashPassword(
            user,
            dto.Password
        );

    await _userRepository.UpdateAsync(user);
    await _userRepository.SaveChangesAsync();
}


        // --------------------------------------
        // Determine role
        // --------------------------------------

        var role =
            user.Role?.RoleName;
            if(string.IsNullOrWhiteSpace(role))
        {
            throw new Exception("User role is not configured.");
        }


        // --------------------------------------
        // Generate JWT
        // --------------------------------------

        var identifier =
            student?.StudentId
            ?? user.Email;


        var token =
            _jwtService.GenerateToken(
                user.UserId,
                identifier,
                role
            );


        return new AuthenticationResponseDto
        {
            Token = token,

            User = MapToDto(
                user,
                role
            )
        };
    }


    // ==========================================
    // MAP USER
    // ==========================================

    private static UserDto MapToDto(
        User user,
        string role)
    {
        return new UserDto
        {
            UserId = user.UserId,

            FullName = user.FullName,

            Role = role,

            Department = string.Empty,

            IsActive =
                user.Status == UserStatus.Active
        };
    }
}
