using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Backend_HU4.Domain.Entities;

public class Owner
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar(100)")] public string Name { get; set; }
    [Column(TypeName = "varchar(100)")] public string Email { get; set; }
}