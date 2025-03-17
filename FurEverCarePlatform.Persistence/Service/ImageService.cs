using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FurEverCarePlatform.Application.Features.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Service;

public class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    public ImageService(IOptions<CloudinarySettings> options)
    {
        var acc = new Account(options.Value.CloudName, options.Value.ApiKey, options.Value.ApiSecret);
        _cloudinary = new Cloudinary(acc);

    }
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "da-net8"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        return uploadResult.Url.ToString();
    }
}
