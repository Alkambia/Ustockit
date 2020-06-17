using Hangfire;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessBatchFile : IProcessBatchFile
    {
        public async Task Execute<T>(T args)
        {
            var rows = args as List<JToken>;
            foreach (var row in rows)
            {
                BackgroundJob.Enqueue<IProcessProduct>(e => e.Execute(row));
            }
        }
    }
}
