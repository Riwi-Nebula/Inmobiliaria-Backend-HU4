using Inmobiliaria_Backend_HU4.Application.DTOs;

namespace Inmobiliaria_Backend_HU4.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto> CreateAsync(PropertyDto propertyDto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, PropertyDto propertyDto);
    Task<IEnumerable<PropertyDto>> GetAllAsync();
    Task<PropertyDto?> GetByIdAsync(int id);
}