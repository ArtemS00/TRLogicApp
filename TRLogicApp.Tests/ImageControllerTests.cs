using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.IO;
using System.Linq;
using TRLogicApp.Controllers;
using TRLogicApp.Entities;
using TRLogicApp.Models;
using TRLogicApp.Services;
using Xunit;
using ImageConverter = TRLogicApp.Services.ImageConverter;

namespace TRLogicApp.Tests
{
    public class ImageControllerTests
    {
        private ImageModel[] _imageModels = new ImageModel[]
        {
            new ImageModel() // sample1 image
            {
                Base64Data = Convert.ToBase64String(ImageConverter.FromImage(Resources.sample1))
            },
            new ImageModel() // sample2 image
            {
                Base64Data = Convert.ToBase64String(ImageConverter.FromImage(Resources.sample2))
            },
            new ImageModel() // url image
            {
                Url = "https://static.toiimg.com/photo/72975551.cms"
            }
        };

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void GetNonexistent(int id)
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);
            var image = controller.Get(id);
            Assert.Null(image);
        }

        [Fact]
        public void GetAllWithEmptyCollection()
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);
            var images = controller.Get();
            Assert.True(images.Count() == 0);
        }

        [Fact]
        public void PostOne()
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);
            var result = controller.Post(new[] { _imageModels[0] }) as StatusCodeResult;
            Assert.InRange(result.StatusCode, 200, 299);
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()), Times.Once());
        }

        [Fact]
        public void PostMany()
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);
            var result = controller.Post(new[] { _imageModels[0], _imageModels[1] }) as StatusCodeResult;
            Assert.InRange(result.StatusCode, 200, 299);
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()), Times.Exactly(2));
        }

        [Fact]
        public void PostManyWithDifferentTypes()
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);
            var result = controller.Post(new[] { _imageModels[0], _imageModels[1], _imageModels[2] }) as StatusCodeResult;
            Assert.InRange(result.StatusCode, 200, 299);
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()), Times.Exactly(3));
        }

        [Fact]
        public void PostOneForm()
        { 
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);

            var files = new FormFileCollection();
            var stream = new MemoryStream(Convert.FromBase64String(_imageModels[0].Base64Data));
            files.Add(new FormFile(stream, 0, stream.Length, "sample1", "sample1"));

            var result = controller.Post(files) as StatusCodeResult;

            Assert.InRange(result.StatusCode, 200, 299);
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()), Times.Once());
        }

        [Fact]
        public void PostManyForm()
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);

            var files = new FormFileCollection();
            for (int i = 0; i < 2; i++)
            {
                var stream = new MemoryStream(Convert.FromBase64String(_imageModels[i].Base64Data));
                files.Add(new FormFile(stream, 0, stream.Length, "sample1", "sample1"));
            }

            var result = controller.Post(files) as StatusCodeResult;

            Assert.InRange(result.StatusCode, 200, 299);
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()), Times.Exactly(2));
        }
    }
}
