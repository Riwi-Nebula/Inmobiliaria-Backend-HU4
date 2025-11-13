using Inmobiliaria_Backend_HU4.Application.DTOs;

namespace Inmobiliaria_Backend_HU4.Application.Interfaces;

public interface IOwnerService
{
    Task<OwnerDto> CreateAsync(OwnerDto ownerDto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, OwnerDto ownerDto);
    Task<IEnumerable<OwnerDto>> GetAllAsync();
    Task<OwnerDto?> GetByIdAsync(int id);

}