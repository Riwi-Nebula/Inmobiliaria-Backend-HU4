using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;

namespace Inmobiliaria_Backend_HU4.Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    //obtener todos los clientes
    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllCustomersAsync();

        return customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        });
    }

    //obtener cliente por id
    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null) return null;

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone
        };
    }

    //crear nuevo cliente
    public async Task<CustomerDto> AddCustomerAsync(AddCustomerDto dto)
    {
        var hashedPassword = HashPassword(dto.Password);

        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = hashedPassword,
            RegistrationDate = DateTime.UtcNow
        };

        await _customerRepository.AddCustomerAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone
        };
    }

    private string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
    
    //actualizar customer
    public async Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto dto)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null) return null;

        customer.Name = dto.Name;
        customer.Phone = dto.Phone;

        await _customerRepository.UpdateCustomerAsync(customer);
        await _customerRepository.SaveChangesAsync();

        // Mapear a DTO
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone
        };
    }


    // Eliminar cliente
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null) return false;

        await _customerRepository.DeleteCustomerAsync(id);
        await _customerRepository.SaveChangesAsync();

        return true;
    }

}