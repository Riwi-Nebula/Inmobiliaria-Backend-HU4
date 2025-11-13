using Inmobiliaria_Backend_HU4.Domain.Entities;

namespace Inmobiliaria_Backend_HU4.Domain.Interfaces;

public interface ICustomerRepository
{
    Task AddCustomerAsync(Customer customer);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(int id);
    Task SaveChangesAsync();
}