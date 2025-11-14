using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Data.Configurations;

public class PropertyConfigurations : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        //Se especifica la PK que va a tener la tabla
        builder.HasKey(p => p.Id);
        
        //Se configuran las otras propiedades
        builder.Property(p => p.Tittle)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(900);
        
        builder.Property(p => p.Location)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(900);

        // builder.Property(p => p.PictureUrl)
        //     .IsRequired();

        builder.Property(p => p.PictureUrl)
            .HasColumnType("json");
        
        builder.Property(p => p.OwnerId)
            .IsRequired();
        
        //Crear una Property de prueba:
        builder.HasData(
            new Property
            {
                Id = 1,
                Tittle = "Torre Stark",
                Description = "Antigua torre de los vengadores",
                Type = PropertyType.Otro,
                State = PropertyState.Disponible,
                Location = "NewYork",
                Address = "200 Park Ave S, Nueva York, NY 10003, EE. UU",
                //TODO PictureUrl = ""
                Price = 180,
                OwnerId = 2
            }
        );
    }
}