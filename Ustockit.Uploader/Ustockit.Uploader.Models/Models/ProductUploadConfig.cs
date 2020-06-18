using System;
using System.Collections.Generic;
using System.Text;

namespace Ustockit.Uploader.Models.Models
{
    public class ProductUploadConfig
    {
        public ExcelConfig ExcelConfig { get; set; }

        public XmlConfig XmlConfig { get; set; }
    }

    public class ExcelConfig
    {
        public int StartingRow { get; set; }
        public int ProductNoCol { get; set; }
        public int DescriptionCol { get; set; }
        public int PriceCol { get; set; }
    }

    public class XmlConfig
    {
        public string HeadTag { get; set; }
        public string[] Property { get; set; }
    }
}
