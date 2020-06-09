using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Infrastructure.Abstract;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Concrete
{
    public class JsonParser : IParser
    {
        public Task<IList<ProductModel>> ParseAsync(string file)
        {
            var test = new ProductModel();

            throw new NotImplementedException();
        }
    }
}
