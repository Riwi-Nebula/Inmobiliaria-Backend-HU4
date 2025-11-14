using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Interfaces;

namespace Inmobiliaria_Backend_HU4.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController : ControllerBase
{
    private readonly IOwnerService _service;
    public OwnerController(IOwnerService ownerService)
    {
        _service = ownerService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var owners = await _service.GetAllAsync();
            return Ok(owners);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo traer todos los propietarios {e}" });
        }
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var owner = await _service.GetByIdAsync(id);
            
            if (owner == null)
                return NotFound(new { message = $"No existe el propietario con ID {id}" });

            return Ok(owner);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo traer el propietario con el ID: {id} {e}" });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] OwnerDto createDto)
    {
        try
        {
            var newOwner = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = newOwner.Id }, newOwner);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo crear el propietario {e}" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] OwnerDto ownerDto)
    {
        try
        {
            var updateOwner = await _service.UpdateAsync(id, ownerDto);
            return Ok($"Propietario actualizado exitosamente{updateOwner}");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo actualizar el propietario con ID: {id} {e}" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok("EL propietario fue eliminado exitosamente");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo eliminar el propietario con ID: {id} {e}" });
        }
    }
}
