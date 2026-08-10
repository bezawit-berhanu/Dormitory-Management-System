using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class QRCodeController : ControllerBase
{
    private readonly IQRCodeService _service;


    public QRCodeController(IQRCodeService service)
    {
        _service = service;
    }



    // Generate QR Code
    [HttpPost("generate/{studentId}")]
    public async Task<IActionResult> Generate(int studentId)
    {
        await _service.GenerateQRCodeAsync(studentId);

        return Ok("QR Code generated successfully");
    }



    // Get student's QR Code
    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetQRCode(int studentId)
    {
        var qrCode = await _service.GetQRCodeAsync(studentId);


        if(qrCode == null)
            return NotFound();


        return Ok(qrCode);
    }



    // Validate QR Code
    [HttpPost("validate")]
    public async Task<IActionResult> Validate(string qrCodeValue)
    {
        var result = await _service.ValidateQRCodeAsync(qrCodeValue);


        if(!result)
            return BadRequest("Invalid QR Code");


        return Ok("Valid QR Code");
    }
}