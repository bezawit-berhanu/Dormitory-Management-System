using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;


namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InspectionController : ControllerBase
{

    private readonly IInspectionService _service;



    public InspectionController(
        IInspectionService service)
    {
        _service = service;
    }




    // GET ALL INSPECTIONS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var result =
            await _service.GetAllAsync();


        return Ok(result);

    }




    // CREATE INSPECTION

    [HttpPost]
    public async Task<IActionResult> Create(
        InspectionDto dto)
    {

        await _service.CreateAsync(dto);


        return Ok(new
        {
            message = "Inspection created successfully"
        });

    }
    //update inspection
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        InspectionDto dto)
    {

        await _service.UpdateAsync(id, dto);
        return Ok(new
        {
            message = "Inspection updated successfully"
        });
    }



    // DELETE

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {

        await _service.DeleteAsync(id);


        return Ok(new
        {
            message = "Inspection deleted"
        });

    }

}