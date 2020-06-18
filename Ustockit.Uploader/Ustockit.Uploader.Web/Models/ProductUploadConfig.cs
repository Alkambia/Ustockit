using System;
using System.Collections.Generic;
using System.Text;

namespace Ustockit.Uploader.Models.Models
{
    public class ProductUploadConfig
    {
        public int StartingRow { get; set; }

        public int ProductNoCol { get; set; }
        public int DescriptionCol { get; set; }
        public int PriceCol { get; set; }
    }
}
