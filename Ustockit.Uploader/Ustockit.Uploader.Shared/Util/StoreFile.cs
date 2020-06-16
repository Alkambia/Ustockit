using Hangfire;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.JobProcessor.Jobs;
using Ustockit.Uploader.Shared.Models;

namespace Ustockit.Uploader.Shared.Util
{
    public class StoreFile : IStoreFile
    {
        public async Task SaveToStorage(IList<IFormFile> files, string directoryPath)
        {
            var exts = new List<string>() { ".csv", ".json", ".xlsx", "xml" };
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (exts.Contains(extension))
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            var FileId = Guid.NewGuid();
                            var filePath = Path.Combine(directoryPath, FileId.ToString());
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);

                                //Populate StoredFile
                                var storedFile = new StoredFile()
                                {
                                    Id = FileId,
                                    FileName = file.FileName,
                                    Extension = extension,
                                    Directory = directoryPath
                                };

                                //enqueue
                                BackgroundJob.Enqueue<IProcessFileStored>(e => e.Execute(storedFile));
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Unsupported file Extension");
                    }
                }
            }
        }
    }
}
