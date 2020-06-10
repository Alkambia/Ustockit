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

namespace Ustockit.Uploader.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
            var exts = new List<string>() { ".csv", ".json", ".xlsx" };
            var files = Request.Form.Files;
            string message = "Successfully Uploaded";
            int status = StatusCodes.Status200OK;

            foreach(var file in files)
            {
                if(file.Length > 0)
                {
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    if(file.ContentType.Equals("application/octet-stream") && exts.Contains(extension))
                    {
                        byte[] fileBytes;
                        using (var stream = file.OpenReadStream())
                        {
                            fileBytes = stream.ReadAllBytes();
                            var binaryObject = new BinaryObject(fileBytes);
                        }
                    }
                    else
                    {
                        status = StatusCodes.Status415UnsupportedMediaType;
                        message = "Unsupported file Extension";
                        break;
                    }
                }
            }

            await Task.FromResult(true);
            var msg = new { Status = status, Message = message };
            return Json(msg);
        }
    }
}
