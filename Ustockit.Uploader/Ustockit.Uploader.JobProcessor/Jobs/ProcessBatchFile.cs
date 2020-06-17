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
            JObject jbatch = args as JObject;
            if(jbatch.ContainsKey("BatchRows"))
            {
                var rows = jbatch["BatchRows"] as JArray;
                foreach(var row in rows)
                {
                    //process product
                    //enqueue
                    BackgroundJob.Enqueue<IProcessProduct>(e => e.Execute(row));
                }
            }
        }
    }
}
