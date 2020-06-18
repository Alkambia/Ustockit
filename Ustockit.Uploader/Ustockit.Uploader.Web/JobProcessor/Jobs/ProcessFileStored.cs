using Hangfire;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Shared.Models;
using Ustockit.Uploader.Web.Infrastructure.Concrete;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessFileStored : IProcessFileStored
    {
        private IConfigurationRoot _appConfiguration;
        private ExcelParser _excelParser;
        public ProcessFileStored(ExcelParser excelParser, IConfigurationRoot appConfiguration)
        {
            _excelParser = excelParser;
            _appConfiguration = appConfiguration;
        }

        public async Task Execute<T>(T args)
        {
            var products = new List<ProductModel>();
            var storedfile = args as StoredFile;
            var filePath = string.Format("{0}\\{1}", storedfile.Directory,storedfile.Id);

            #region Parser code here
            //exceute, from storage to batch queue
            switch (storedfile.Extension)
            {
                case ".csv":
                    {
                        break;
                    }
                case ".json":
                    {
                        break;
                    }
                case ".xlsx": 
                case ".xls":
                    {
                        products = (await _excelParser.ParseAsync(filePath, "seco")).ToList();
                        break;
                    }

                case ".xml":
                    {
                        break;
                    }
            }
            #endregion


            var skip = 0;
            //from config
            var take = 5000;
            int.TryParse(_appConfiguration["App:BatchMaxRows"], out take);

            while (skip < products.Count())
            {
                var takenList = products.Skip(skip).Take(take);
                var batchList = takenList.ToList();
                //enqueue
                BackgroundJob.Enqueue<IProcessBatchFile>(e => e.Execute(batchList));
                skip = (skip + take);
            }

        }
    }
}