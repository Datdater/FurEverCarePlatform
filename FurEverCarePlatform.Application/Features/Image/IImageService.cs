using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Image;

public interface IImageService
{
    public Task<string> UploadImageAsync(IFormFile file);
}
