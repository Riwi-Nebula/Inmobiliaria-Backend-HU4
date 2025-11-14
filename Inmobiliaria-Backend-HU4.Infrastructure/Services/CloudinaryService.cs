using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Inmobiliaria_Backend_HU4.Application.DTOs;
using Inmobiliaria_Backend_HU4.Application.Interfaces;
using Inmobiliaria_Backend_HU4.Infrastructure.Settings;

namespace Inmobiliaria_Backend_HU4.Infrastructure.Services;

public class CloudinaryService : IClouddinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<string> UploadImageAsync(UploadFileDto file)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.FileStream),
            Folder = "properties"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
            throw new Exception(uploadResult.Error.Message);

        return uploadResult.SecureUrl.AbsoluteUri;
    }
}