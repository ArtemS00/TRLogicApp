using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            },
            new ImageModel() // sample1 image + url image (incorrect model)
            {
                Base64Data = Convert.ToBase64String(ImageConverter.FromImage(Resources.sample1)),
                Url = "https://static.toiimg.com/photo/72975551.cms"
            },
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
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()));
        }

        [Fact]
        public void PostMany()
        {
            var mock = new Mock<IRepository<ImageEntity>>();
            var controller = new ImagesController(mock.Object);
            var result = controller.Post(new[] { _imageModels[0], _imageModels[0] }) as StatusCodeResult;
            Assert.InRange(result.StatusCode, 200, 299);
            mock.Verify(i => i.Add(It.IsAny<ImageEntity>()), Times.Exactly(2));
        }
    }
}
