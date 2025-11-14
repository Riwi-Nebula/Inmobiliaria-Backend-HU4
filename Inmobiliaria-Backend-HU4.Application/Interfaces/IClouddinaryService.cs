using Inmobiliaria_Backend_HU4.Application.DTOs;

namespace Inmobiliaria_Backend_HU4.Application.Interfaces;

public interface IClouddinaryService
{
    Task<string> UploadImageAsync(UploadFileDto file);
}