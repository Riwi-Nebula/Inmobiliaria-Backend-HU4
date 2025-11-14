using Inmobiliaria_Backend_HU4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Data.Configurations;

public class OwnerConfigurations : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        //Se le especifica a la db que la tabla tiene una llave primaria osea el ID de Owner
        builder.HasKey(o => o.Id);
        
        //Se especifica si una propiedad es requerida y el tamaño maximo de la cadena
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(o => o.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        //Se crea un indice para que esta propiedad sea unica
        builder.HasIndex(o => o.Email)
            .IsUnique();
        
        //Crear un Owner de prueba:
        builder.HasData(
            new Owner
            {
                Id = 1,
                Name = "Bruce Wayne",
                Email = "bruce.wayne@gmail.com"
            },
            new Owner
            {
                Id = 2,
                Name = "Happy Hogan",
                Email = ""
            }
        );
    }
}