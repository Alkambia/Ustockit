using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ustockit.Uploader.Shared.Util;
using Ustockit.Uploader.Web.Controllers;
using Ustockit.Uploader.Web.Infrastructure.Concrete;
using Ustockit.Uploader.Web.Infrastructure.Ext;
using Ustockit.Uploader.Web.Models;
using Xunit;

namespace Ustockit.Uploader.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }


        [Fact]
        public async Task UploadPost_ReturnsJsonResult()
        {
            // Arrange
            var mocklogger = new Mock<ILogger<HomeController>>();
            //mocklogger.Setup(request => request.LogInformation("Test",null));
            var storeFile = new Mock<IStoreFile>();
            var excelParser = new Mock<ExcelParser>();

            var controller = new HomeController(mocklogger.Object, storeFile.Object, excelParser.Object);

            // Act
            var result = await controller.Upload();

            // Assert
            var json = Assert.IsType<JsonResult>(result);
            Assert.IsType<JsonResult>(json.Value);
        }

        //[Fact]
        //public async Task UploadPost_SaveFile()
        //{
        //    // Arrange
        //    var mockSaveFile = new Mock<HomeController>();
        //    mockSaveFile.Setup(thing => thing.ControllerContext);

        //    var mockFile = new Mock<IFormFile>();
        //    mockFile.SetupGet(file => file);

        //    // Act
        //    try
        //    {
        //        string extension = System.IO.Path.GetExtension(mockFile.Object.FileName);
        //        byte[] fileBytes;
        //        using (var stream = mockFile.Object.OpenReadStream())
        //        {
        //            fileBytes = stream.ReadAllBytes();
        //            var binaryObject = new BinaryObject(fileBytes);
        //            await mockSaveFile.Object.SaveFileAsync(extension, mockFile.Object, binaryObject);
        //        }

        //        Assert.False(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.False(true,ex.Message);
        //    }
        //}

        [Fact]
        public void UploadReturnsViewResult()
        {
            // Arrange
            var mocklogger = new Mock<ILogger<HomeController>>();
            //mocklogger.Setup(request => request.LogInformation("Test",null));
            var storeFile = new Mock<IStoreFile>();
            var excelParser = new Mock<ExcelParser>();

            var controller = new HomeController(mocklogger.Object, storeFile.Object, excelParser.Object);

            // Act
            var result = controller.FileUpload();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
