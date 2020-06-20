using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Repositories
{
    public interface IRepositoryBase
    {
        Task InsertProduct(ProductModel model);
    }
}
