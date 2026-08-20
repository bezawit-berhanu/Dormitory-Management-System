using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;


namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/users")]
public class UserManagementController : ControllerBase
{

    private readonly IUserManagementService _service;


    public UserManagementController(IUserManagementService service)
    {
        _service = service;
    }



    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _service.GetAllUsersAsync();

        return Ok(users);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _service.GetUserByIdAsync(id);


        if(user == null)
            return NotFound();


        return Ok(user);
    }



    [HttpPut]
    public async Task<IActionResult> Update(UserDto dto)
    {
        var result = await _service.UpdateUserAsync(dto);


        if(!result)
            return NotFound();


        return Ok("User updated successfully");
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteUserAsync(id);


        if(!result)
            return NotFound();


        return Ok("User deleted successfully");
    }
}