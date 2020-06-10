using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Abstract
{
    public  interface IParser
    {
        Task<IList<ProductModel>> ParseAsync(string file, BinaryObject binaryObject);
    }
}
