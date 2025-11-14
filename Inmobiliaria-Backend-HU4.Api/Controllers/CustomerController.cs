using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Backend_HU4.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [Authorize]
        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var loggedRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            //solo admin puede ver todos los clientes
            if (loggedRole != "Admin")
                return Unauthorized(new { message = "No estás autorizado para ver todos los clientes." });
            
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // admin puede ver todos y customer solo su mismo id
        [Authorize(Roles = "Admin,Customer")]
        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var loggedUserId = int.Parse(User.FindFirst("id").Value);
            var loggedRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            //Customer solo puede ver su propio perfil
            if (loggedRole == "Customer" && loggedUserId != id)
                return Unauthorized(new { message = "No estás autorizado para ver este perfil." });
            
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            return Ok(customer);
        }

        
        [Authorize]
        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync([FromBody] AddCustomerDto dto)
        {
            var loggedRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            //solo admin puede crear nuevos clientes
            if (loggedRole != "Admin")
                return Unauthorized(new { message = "No estás autorizado para crear clientes." });
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _customerService.AddCustomerAsync(dto);
            return Created($"/api/customer/{created.Id}", created);
        }

        //admin puede editar todos y customer solo su propio id
        [Authorize(Roles = "Admin,Customer")]
        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int id, [FromBody] UpdateCustomerDto dto)
        {
            var loggedUserId = int.Parse(User.FindFirst("id").Value);
            var loggedRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            // Customer solo puede editar su propio perfil
            if (loggedRole == "Customer" && loggedUserId != id)
                return Unauthorized(new { message = "No estás autorizado para editar este perfil." });

            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, dto);
            if (updatedCustomer == null)
                return NotFound(new { message = "Customer not found." });

            return Ok(updatedCustomer);
        }
        
        [Authorize]
        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            
            var loggedRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            //solo admin puede eliminar clientes
            if (loggedRole != "Admin")
                return Unauthorized(new { message = "No estás autorizado para eliminar clientes." });
            
            var deleted = await _customerService.DeleteCustomerAsync(id);
            if (!deleted)
                return NotFound(new { message = "Customer not found." });

            return NoContent();
        }
    }
}
