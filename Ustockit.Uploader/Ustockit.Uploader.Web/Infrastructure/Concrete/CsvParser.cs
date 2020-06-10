using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Infrastructure.Abstract;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Concrete
{
    public class CsvParser : IParser
    {
        public async Task<IList<ProductModel>> ParseAsync(string file, BinaryObject binaryObject)
        {
            throw new NotImplementedException();
        }
    }
}
