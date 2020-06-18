using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessProduct : IProcessProduct
    {
        public async Task Execute<T>(T args)
        {
            //Apply validation
            //process here
            //or send to API
        }
    }
}
