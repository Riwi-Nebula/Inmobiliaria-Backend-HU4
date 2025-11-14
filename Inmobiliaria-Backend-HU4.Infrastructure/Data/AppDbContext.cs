using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    //Creacion de las tablas:
    //Ejemplo: public DbSet<User> Users { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    //Se usa las configuraciones de la api para la conexion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Se apilican las configuraciones Fluent API desde la carpeta Configurations
        //Ejemplo: modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OwnerConfigurations());
        modelBuilder.ApplyConfiguration(new PropertyConfigurations());
        
        var converter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null)!
        );

        var comparer = new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );

        modelBuilder.Entity<Property>()
            .Property(p => p.PictureUrl)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);
    }
}