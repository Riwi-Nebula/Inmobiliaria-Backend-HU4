using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Interfaces;

namespace Inmobiliaria_Backend_HU4.Api.Controllers;

public class PropertyController : ControllerBase
{
    private readonly IPropertyService _service;
    public PropertyController(IPropertyService propertyService)
    {
        _service = propertyService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Customer")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var propieties = await _service.GetAllAsync();
            return Ok(propieties);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo traer todos los propietarios {e}" });
        }
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin, Customer")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var property = await _service.GetByIdAsync(id);
            
            if (property == null)
                return NotFound(new { message = $"No existe la propiedad con ID {id}"})

            return Ok(property);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo traer el propietario con el ID: {id} {e}" });
        }
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] PropertyDto propertyDto)
    {
        try
        {
            var newProperty = await _service.CreateAsync(propertyDto);
            return CreatedAtAction(nameof(GetById), new { id = newProperty.Id }, newProperty);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"No se pudo crear la propiedad {e}" });
        }
    }
    
    
    
}