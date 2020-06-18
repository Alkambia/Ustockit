using Hangfire;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessBatchFile : IProcessBatchFile
    {
        public async Task Execute<T>(T args)
        {
            //var rows = args as List<T>;
            var rows = args as List<ProductModel>;
            foreach (var row in rows)
            {
                //use Task.FromResult for async will change later
                await Task.FromResult(BackgroundJob.Enqueue<IProcessProduct>(e => e.Execute(row)));
            }
        }
    }
}
