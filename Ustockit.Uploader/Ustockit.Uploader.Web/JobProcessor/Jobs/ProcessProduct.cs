using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Models;
using Ustockit.Uploader.Web.Repositories;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public class ProcessProduct : IProcessProduct
    {
        private IRepositoryBase _repo;
        public ProcessProduct(IRepositoryBase repo)
        {
            _repo = repo;
        }
        public async Task Execute<T>(T args)
        {
            var product = args as ProductModel;
            await _repo.InsertProduct(product);
            //Apply validation
            //process here
            //or send to API
        }
    }
}
