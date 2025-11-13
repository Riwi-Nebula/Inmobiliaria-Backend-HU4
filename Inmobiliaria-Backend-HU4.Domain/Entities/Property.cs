using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inmobiliaria_Backend_HU4.Domain.Enums;

namespace Inmobiliaria_Backend_HU4.Domain.Entities;

public class Property
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "Varchar(100)")] public string Tittle { get; set; }
    [Column(TypeName = "varchar(900)")] public string Description { get; set; }
    public PropertyType Type { get; set; }
    [Column(TypeName = "varchar(500)")] public string Location { get; set; }
    [Column(TypeName = "text")] public string PictureUrl { get; set; }
    [Column(TypeName = "decimal(5,2)")] public float Price { get; set; }
    public int OwnerId { get; set; }
    
    //Foreankey
    public Owner? Owner { get; set; }
}
