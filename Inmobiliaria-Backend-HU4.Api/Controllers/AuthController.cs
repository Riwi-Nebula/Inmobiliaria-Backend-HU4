using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Services;
using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Inmobiliaria_Backend_HU4.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly TokenService _tokenService;

        public AuthController(ICustomerRepository customerRepository, TokenService tokenService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email y contraseña son requeridos.");

            var existing = await _customerRepository.GetCustomerByEmailAsync(dto.Email);
            if (existing != null)
                return Conflict("Ya existe un usuario con este correo.");

            // Hashear contraseña
            var hashedPassword = HashPassword(dto.Password);

            // Validar rol
            var role = dto.Role?.Trim().ToLower();
            if (role != "admin" && role != "customer")
                return BadRequest("El rol debe ser 'Admin' o 'Customer'.");

            var newCustomer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Phone = dto.Phone,
                Role = dto.Role
            };

            await _customerRepository.AddCustomerAsync(newCustomer);
            await _customerRepository.SaveChangesAsync();

            var token = _tokenService.GenerateToken(newCustomer.Email, newCustomer.Role);

            return Ok(new
            {
                token,
                newCustomer.Email,
                newCustomer.Role
            });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(dto.Email);
            if (customer == null)
                return Unauthorized("Credenciales inválidas.");

            var hash = HashPassword(dto.Password);
            if (hash != customer.PasswordHash)
                return Unauthorized("Credenciales inválidas.");

            var token = _tokenService.GenerateToken(customer.Email, customer.Role);

            return Ok(new
            {
                token,
                customer.Email,
                customer.Role
            });
        }

        // Hashear contraseñas con SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
