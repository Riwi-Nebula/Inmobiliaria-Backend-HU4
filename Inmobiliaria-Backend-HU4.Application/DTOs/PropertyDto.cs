namespace Inmobiliaria_Backend_HU4.Application.DTOs;

public class PropertyDto
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string State { get; set; }
    public string Location { get; set; }
    public string Address { get; set; }
    public List<string>? Urls { get; set; }
    public float Price { get; set; }
    public int OwnerId { get; set; }
}