using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Shared.Models;

namespace Ustockit.Uploader.Shared.Util
{
    public interface IStoreFile
    {
        Task SaveToStorage(IList<IFormFile> files, string directoryPath);
    }
}
