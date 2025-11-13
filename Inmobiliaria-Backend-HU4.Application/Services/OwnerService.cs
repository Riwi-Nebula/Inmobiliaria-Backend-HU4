using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Interfaces;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;
using Inmobiliaria_Backend_HU4.Domain.Entities;

namespace Inmobiliaria_Backend_HU4.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _repository;

    public OwnerService(IOwnerRepository repository)
    {
        _repository = repository;
    }

    //metodos privados para mapear el dto
    private OwnerDto MapOwnerToDto(Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.Id,
            Name = owner.Name,
            Email = owner.Email
        };
    }
    
    //crear propietario
    public async Task<OwnerDto> CreateAsync(OwnerDto ownerDto)
    {
        try
        {
            var owner = new Owner
            {
                Name = ownerDto.Name,
                Email = ownerDto.Email
            };
            var newOwner = await _repository.AddOwner(owner);
            return MapOwnerToDto(newOwner);
        }
        catch (Exception e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"Ocurio un error creando un propietario:\n{e}\nExploto en OwnerService");
            Console.WriteLine("posible error en infrastructure o application");
            Console.WriteLine("========================================================");
            throw;
        }
    }

    //eliminar un propietario
    public async Task DeleteAsync(int id)
    {
        try
        {
            var deleteOwner = await _repository.DeleteOwner(id);
            if (deleteOwner == null)
                Console.WriteLine($"El propietario con ID: {id} no existe");
        }
        catch (Exception e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"Ocurio un error eliminando un propietario:\n{e}");
            Console.WriteLine("posible error en infrastructure o application");
            Console.WriteLine("========================================================");
            throw;
        }
    }

    public async Task UpdateAsync(int id, OwnerDto ownerDto)
    {
        try
        {
            var owner = await _repository.GetOwnerById(id);
            if (owner != null)
            {
                owner.Name = ownerDto.Name;
                owner.Email = ownerDto.Email;

                await _repository.UpdateOwner(owner);
            }
            else
            {
                Console.WriteLine($"El propietario que se intenta actualizar es nulo");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"Ocurio un error actualizando un propietario:\n{e}");
            Console.WriteLine("posible error en infrastructure o application");
            Console.WriteLine("========================================================");
            throw;
        }
    }

    public async Task<IEnumerable<OwnerDto>> GetAllAsync()
    {
        try
        {
            var owners = await _repository.GetAllOwners();
            return owners.Select(MapOwnerToDto).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"Ocurio un error  un propietario:\n{e}");
            Console.WriteLine("posible error en infrastructure o application");
            Console.WriteLine("========================================================");
            throw;
        }
    }

    public async Task<OwnerDto?> GetByIdAsync(int id)
    {
        try
        {
            var owner = await _repository.GetOwnerById(id);
            if (owner != null)
            {
                return MapOwnerToDto(owner);
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"Ocurio un error  un propietario:\n{e}");
            Console.WriteLine("posible error en infrastructure o application");
            Console.WriteLine("========================================================");
            throw;
        }
    }
}