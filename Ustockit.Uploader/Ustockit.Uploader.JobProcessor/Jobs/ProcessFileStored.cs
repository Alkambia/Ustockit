using Hangfire;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Shared.Models;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessFileStored : IProcessFileStored
    {

        public async Task Execute<T>(T args)
        {
            JObject jbatch = new JObject();

            var storedfile = args as StoredFile;
            //exceute, from storage to batch queue
            switch (storedfile.Extension)
            {
                case ".csv":
                    {
                        //10k or less
                        //perform parse
                        //add data to jbatch or create a model
                        //IE:
                        JObject data = new JObject();
                        data["Id"] = 1;
                        data["Name"] = "Bear Brand";

                        JObject data2 = new JObject();
                        data2["Id"] = 2;
                        data2["Name"] = "Nestle";


                        jbatch["BatchRows"] = new JArray()
                        {
                            data,
                            data2
                        };
                        break;
                    }
                case ".json":
                    {
                        //10k or less
                        //perform parse
                        //add data to jbatch or create a model
                        //IE:
                        JObject data = new JObject();
                        data["Id"] = 1;
                        data["Name"] = "Bear Brand";

                        JObject data2 = new JObject();
                        data2["Id"] = 2;
                        data2["Name"] = "Nestle";


                        jbatch["BatchRows"] = new JArray()
                        {
                            data,
                            data2
                        };
                        break;
                    }
                case ".xlsx":
                    {
                        //10k or less
                        //perform parse
                        //add data to jbatch or create a model
                        //IE:
                        JObject data = new JObject();
                        data["Id"] = 1;
                        data["Name"] = "Bear Brand";

                        JObject data2 = new JObject();
                        data2["Id"] = 2;
                        data2["Name"] = "Nestle";


                        jbatch["BatchRows"] = new JArray()
                        {
                            data,
                            data2
                        };
                        break;
                    }

                case "xml":
                    {
                        //10k or less
                        //perform parse
                        //add data to jbatch or create a model
                        //IE:
                        JObject data = new JObject();
                        data["Id"] = 1;
                        data["Name"] = "Bear Brand";

                        JObject data2 = new JObject();
                        data2["Id"] = 2;
                        data2["Name"] = "Nestle";


                        jbatch["BatchRows"] = new JArray()
                        {
                            data,
                            data2
                        };
                        break;
                    }
            }

            //enqueue
            //should apply batch concept here
            BackgroundJob.Enqueue<IProcessBatchFile>(e => e.Execute(jbatch));
        }
    }
}
