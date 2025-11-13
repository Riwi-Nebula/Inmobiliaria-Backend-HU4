using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;
using Inmobiliaria_Backend_HU4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Repositories;

 public class CustomerRepository : ICustomerRepository
 {
     private readonly AppDbContext _context;
     public CustomerRepository(AppDbContext context)
     {
         _context = context;
     }
     
     public async Task AddCustomerAsync(Customer customer)
     {
         await _context.Customers.AddAsync(customer);
     }

     public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
     {
         return await _context.Customers.ToListAsync();
     }

     public async Task<Customer?> GetCustomerByEmailAsync(string email)
     {
         return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
     }

     public async Task<Customer?> GetCustomerByIdAsync(int id)
     {
         return await _context.Customers.FindAsync(id);
     }

     public async Task UpdateCustomerAsync(Customer customer)
     {
         _context.Customers.Update(customer);
         await _context.SaveChangesAsync();
     }

     public async Task DeleteCustomerAsync(int id)
     {
         var customer = await _context.Customers.FindAsync(id);
         if (customer != null)
         {
             _context.Customers.Remove(customer);
         }
     }

     public async Task SaveChangesAsync()
     {
         await _context.SaveChangesAsync();
     }
}