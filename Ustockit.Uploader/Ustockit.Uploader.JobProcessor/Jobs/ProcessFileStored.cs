using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Shared.Models;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessFileStored : IProcessFileStored
    {

        public async Task Execute(StoredFile storedfile)
        {
            //exceute, from storage to batch queue
        }
    }
}
