using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;


namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{

    private readonly INotificationService _service;



    public NotificationController(
        INotificationService service)
    {

        _service = service;

    }



    // Get user notifications
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserNotifications(
        int userId)
    {

        var result =
            await _service.GetUserNotificationsAsync(userId);


        return Ok(result);

    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {

        var result =
            await _service.GetAllAsync();


        return Ok(result);

    }


    // Create notification
    [HttpPost]
    public async Task<IActionResult> Create(
        NotificationDto dto)
    {

        await _service.CreateAsync(dto);


        return Ok(new
        {
            notificationId = dto.NotificationId,
            userId = dto.UserId,
            message = dto.Message,
            isRead = dto.IsRead,
            createdDate = dto.CreatedDate
        });


    }




    // Mark notification as read
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(
        int id)
    {

        await _service.MarkAsReadAsync(id);


        return Ok(new
        {
            message = "Notification marked as read"
        });

    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserNotifications(
        int userId)
    {

        var result =
            await _service.GetUserNotificationsAsync(userId);


        return Ok(result);

    }


}