using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Abstract
{
    public interface IFile
    {
        //todo: transfer to another folder, just created this interface fo unit test mock
        //Task SaveFileAsync(string extension, IFormFile file, BinaryObject binaryObject);
    }
}
