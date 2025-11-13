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

        //solo admin puede ver todos los clientes
        [Authorize(Roles = "Admin")]
        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // tanto admin y customer pueden ver su propio perfil
        [Authorize(Roles = "Admin,Customer")]
        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            return Ok(customer);
        }

        //solo admin puede crear nuevos clientes
        [Authorize(Roles = "Admin")]
        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync([FromBody] AddCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _customerService.AddCustomerAsync(dto);
            return Created($"/api/customer/{created.Id}", created);
        }

        //tanto admin y customer pueden actualizar su perfil
        [Authorize(Roles = "Admin,Customer")]
        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int id, [FromBody] UpdateCustomerDto dto)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, dto);
            if (updatedCustomer == null)
                return NotFound(new { message = "Customer not found." });

            return Ok(updatedCustomer);
        }

        //solo admin puede eliminar clientes
        [Authorize(Roles = "Admin")]
        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);
            if (!deleted)
                return NotFound(new { message = "Customer not found." });

            return NoContent();
        }
    }
}
