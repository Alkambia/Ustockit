using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Shared.Models;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public interface IProcessFileStored
    {
        Task Execute(StoredFile storedfile);
    }
}
