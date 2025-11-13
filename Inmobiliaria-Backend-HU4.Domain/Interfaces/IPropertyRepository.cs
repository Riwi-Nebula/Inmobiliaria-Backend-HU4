using Inmobiliaria_Backend_HU4.Domain.Entities;

namespace Inmobiliaria_Backend_HU4.Domain.Interfaces;

public interface IPropertyRepository
{
    public Task<Property> CreateProperty(Property property);
    public Task<Property?> UpdateProperty(Property property);
    public Task<Property?> DeleteProperty(int id);
    public Task<Property?> GetPropertyById(int id);
    public Task<IEnumerable<Property>> GetAllProperties();
}