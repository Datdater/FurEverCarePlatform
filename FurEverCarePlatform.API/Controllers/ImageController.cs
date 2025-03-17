using FurEverCarePlatform.Application.Features.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController(IImageService imageService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File is null");
            }
            if (file.Length == 0)
            {
                return BadRequest("File is empty");
            }
            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("File is too large");
            }
            var result = await imageService.UploadImageAsync(file);
            return Ok(result);
        }
    }
}
