using Inmobiliaria_Backend_HU4.Domain.Entities;
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
        
        
    }
}