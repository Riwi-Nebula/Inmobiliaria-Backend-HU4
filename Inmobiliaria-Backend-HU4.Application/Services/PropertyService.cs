using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Interfaces;
using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Enums;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;

namespace Inmobiliaria_Backend_HU4.Application.Services;
//TODO refactorizar para mejorar optimizacion

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _repository;
    public PropertyService(IPropertyRepository repository)
    {
        _repository = repository;
    }

    private PropertyDto MapPropertyToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            Tittle = property.Tittle,
            Description = property.Description,
            Type = property.Type.ToString(),
            State = property.State.ToString(),
            Location = property.Location,
            Address = property.Address,
            Url = property.PictureUrl,
            Price = property.Price,
            OwnerId = property.OwnerId
        };
    }

    public async Task<PropertyDto> CreateAsync(PropertyDto propertyDto)
    {
        try
        {
            var properties = await _repository.GetAllProperties();
            bool exists = properties.Any(p =>
                p.Tittle == propertyDto.Tittle &&
                p.Location == propertyDto.Location &&
                p.Description == propertyDto.Description &&
                p.Address == propertyDto.Address
            );

            if (exists)
            {
                Console.WriteLine("========================================================");
                throw new InvalidOperationException($"Ya existe una propiedad con esos datos");
            }

            if (!Enum.TryParse<PropertyType>(propertyDto.Type, true, out var propertyType))
            {
                propertyType = PropertyType.Otro;
            }

            if (!Enum.TryParse<PropertyState>(propertyDto.State, true, out var propertyState))
            {
                propertyState = PropertyState.Otro;
            }

            var property = new Property
            {
                Tittle = propertyDto.Tittle,
                Description = propertyDto.Description,
                Type = propertyType,
                State = propertyState,
                Location = propertyDto.Location,
                Address = propertyDto.Address,
                PictureUrl = propertyDto.Url,
                Price = propertyDto.Price,
                OwnerId = propertyDto.OwnerId
            };

            var newProperty = await _repository.CreateProperty(property);

            return MapPropertyToDto(newProperty);
        }
        catch (Exception e)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine($"Ocurrió un error creando la propiedad:\n{e}\nExplotó en PropertyService");
            Console.WriteLine("Posible error en Infrastructure o Application");
            Console.WriteLine("========================================================");
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var deleteProperty = await _repository.DeleteProperty(id);
        if (deleteProperty == null)
        {
            throw new KeyNotFoundException($"Propiedad con el ID:{id} no encontrada para eliminar");
        }
    }

    public async Task UpdateAsync(int id, PropertyDto propertyDto)
    {
        var property = await _repository.GetPropertyById(id);
        if (property == null)
        {
            throw new KeyNotFoundException($"Propiedad con el ID:{id} no encontrada para actualizar");
        }

        if (!Enum.TryParse<PropertyType>(propertyDto.Type, true, out var propertyType))
        {
            propertyType = PropertyType.Otro;
        }

        if (!Enum.TryParse<PropertyState>(propertyDto.State, true, out var propertyState))
        {
            propertyState = PropertyState.Otro;
        }

        property.Tittle = propertyDto.Tittle;
        property.Description = propertyDto.Description;
        property.Type = propertyType;
        property.State = propertyState;
        property.Location = propertyDto.Location;
        property.Address = propertyDto.Address;
        property.PictureUrl = propertyDto.Url;
        property.Price = propertyDto.Price;
        property.OwnerId = propertyDto.OwnerId;


    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync()
    {
        var properties = await _repository.GetAllProperties();
        return properties.Select(MapPropertyToDto).ToList();
    }

    public async Task<PropertyDto?> GetByIdAsync(int id)
    {
        var property = await _repository.GetPropertyById(id);
        if (property == null)
        {
            throw new KeyNotFoundException($"Propiedad con el ID:{id} no encontrada");
        }
        return MapPropertyToDto(property);
    }
}