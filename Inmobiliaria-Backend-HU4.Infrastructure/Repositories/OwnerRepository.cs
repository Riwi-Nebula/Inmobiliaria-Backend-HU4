using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;
using Inmobiliaria_Backend_HU4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly AppDbContext _context;

    public OwnerRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Owner> AddOwner(Owner owner)
    {
        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();
        return owner;
    }

    public async Task<Owner?> DeleteOwner(int id)
    {
        var owner = await _context.Owners.FindAsync(id);
        if (owner != null)
        {
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
        }
        return owner;
    }

    public async Task<Owner?> UpdateOwner(Owner owner)
    {
        _context.Owners.Update(owner);
        await _context.SaveChangesAsync();
        return owner;
    }

    public async Task<IEnumerable<Owner>> GetAllOwners()
    {
        return await _context.Owners.ToListAsync();
    }

    public async Task<Owner?> GetOwnerById(int id)
    {
        return await _context.Owners.FindAsync(id);
    }
}