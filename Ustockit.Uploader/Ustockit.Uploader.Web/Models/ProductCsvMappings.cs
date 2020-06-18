using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;
using Ustockit.Uploader.Models.Models;

namespace Ustockit.Uploader.Web.Models
{
    public class ProductCsvMappings : CsvMapping<ProductModel>
    {
        public ProductCsvMappings(CsvConfig config) : base()
        {
            MapProperty(config.ProductNoCol, x => x.ProductNo);
            MapProperty(config.DescriptionCol, x => x.Description);
            MapProperty(config.PriceCol, x => x.Price);
        }
    }
}
