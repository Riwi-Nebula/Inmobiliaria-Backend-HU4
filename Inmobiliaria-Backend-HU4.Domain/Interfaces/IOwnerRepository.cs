using Inmobiliaria_Backend_HU4.Domain.Entities;

namespace Inmobiliaria_Backend_HU4.Domain.Interfaces;

public interface IOwnerRepository
{
    public Task<Owner> AddOwner(Owner owner);
    public Task<Owner?> DeleteOwner(int id);
    public Task<Owner?> UpdateOwner(Owner owner);
    public Task<IEnumerable<Owner>> GetAllOwners();
    public Task<Owner?> GetOwnerById(int id);
}