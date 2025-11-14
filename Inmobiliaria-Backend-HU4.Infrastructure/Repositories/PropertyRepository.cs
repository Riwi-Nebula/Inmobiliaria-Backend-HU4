using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;
using Inmobiliaria_Backend_HU4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;
    public PropertyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Property> CreateProperty(Property property)
    {
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<Property?> UpdateProperty(Property property)
    {
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<Property?> DeleteProperty(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }

        return property;
    }

    public async Task<Property?> GetPropertyById(int id)
    {
        return await _context.Properties.FindAsync(id);
    }

    public async Task<IEnumerable<Property>> GetAllProperties()
    {
        return await _context.Properties.ToListAsync();
    }
}