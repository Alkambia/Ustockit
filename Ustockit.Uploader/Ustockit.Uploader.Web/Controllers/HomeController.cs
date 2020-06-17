using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ustockit.Uploader.Web.Models;
using Ustockit.Uploader.Web.Infrastructure.Ext;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Ustockit.Uploader.Web.Infrastructure.Concrete;
using Ustockit.Uploader.Web.Infrastructure.Abstract;
using Ustockit.Uploader.Shared.Util;

namespace Ustockit.Uploader.Web.Controllers
{
    public class HomeController : Controller, IFile
    {
        private readonly ILogger<HomeController> _logger;
        IStoreFile _storeFile;
        private ExcelParser _parser;

        public HomeController(ILogger<HomeController> logger, IStoreFile storeFile, ExcelParser parser)
        {
            _logger = logger;
            _storeFile = storeFile;
            _parser = parser;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult FileUpload()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            //todo: make constant string enum
            var files = Request.Form.Files;
            string message = "Successfully Uploaded";
            string status = "Success";

            IList<IFormFile> filesx = files.ToList();
            await _storeFile.SaveToStorage(filesx, Path.GetFullPath(string.Format("{0}\\{1}",Directory.GetCurrentDirectory(),"fileStorage")));


            await Task.FromResult(true);
            var msg = new { Status = status, Message = message };
            return Json(msg);
        }
    }
}
