using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Backend_HU4.Domain.Entities;
using Inmobiliaria_Backend_HU4.Infrastructure.Data.Configurations;

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
    }
}