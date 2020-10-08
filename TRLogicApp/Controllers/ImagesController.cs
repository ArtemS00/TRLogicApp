using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TRLogicApp.Entities;
using TRLogicApp.Models;
using TRLogicApp.Services;

namespace TRLogicApp.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private IRepository<ImageEntity> _repository;

        public ImagesController(IRepository<ImageEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<ImageEntity> Get()
        {
            return _repository.Get();
        }

        [HttpGet("{id}")]
        public ImageEntity Get(int id)
        {
            return _repository.Get(id);
        }

        [HttpPost]
        public ActionResult Post([FromBody]ImageModel[] images)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is invalid");

            // Convert images to byte arrays
            List<byte[]> convertedImages = new List<byte[]>();
            foreach (var image in images)
            {
                if (image.Base64Data != null)
                    convertedImages.Add(ImageConverter.FromBase64(image.Base64Data));
                if (image.Url != null)
                    convertedImages.Add(ImageConverter.FromUrl(image.Url));
            }

            return ValidateAndSaveImages(convertedImages);
        }

        [Route("form")]
        [HttpPost]
        public ActionResult Post(IFormFileCollection images)
        {
            // Validate images
            foreach (var image in images)
                if (image.Length > 1024 * 1024)
                    return BadRequest("The image is too large! The max size is 1 MB.");

            // Convert images to byte arrays
            List<byte[]> convertedImages = ImageConverter.FromFormFile(images).ToList();

            return ValidateAndSaveImages(convertedImages);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        private ActionResult ValidateAndSaveImages(List<byte[]> images)
        {
            if (images.Any(i => i == null))
                return BadRequest("Incorrect image format");
            if (images.Any(i => i.Length > 1024 * 1024))
                return BadRequest("The image is too large! The max size is 1 MB.");

            // Save images
            images.ForEach(image => _repository.Add(new ImageEntity(image)));

            return Ok();
        }
    }
}
