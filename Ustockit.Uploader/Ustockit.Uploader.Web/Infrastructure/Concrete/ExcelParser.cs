using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Ustockit.Uploader.Models.Models;
using Ustockit.Uploader.Shared.Util;
using Ustockit.Uploader.Web.Infrastructure.Abstract;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Concrete
{
    public class ExcelParser : IParser
    {
        private IConfigurationRoot _appConfiguration;
        public ExcelParser(IConfigurationRoot appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }
        public async Task<IList<ProductModel>> ParseAsync(string filePath, string manufacturer)
        {
            try
            {
                List<ProductModel> products = new List<ProductModel>();
                string configPath = $"Configs\\Manufacturer\\{manufacturer}.config.json";
                var path = _appConfiguration["App:ManufacturerConfigPath"];
                if(!string.IsNullOrEmpty(path))
                {
                    configPath = string.Format(path, manufacturer);
                }
                

                var config = await Task.Run(() => JsonFileUtil.GetFileThenParse<ProductUploadConfig>(configPath));

                FileInfo file = new FileInfo(filePath);

                //create a new Excel package from the file
                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                    int endRow = worksheet.Dimension.End.Row + config.StartingRow;
                    for (int i = config.StartingRow; i < endRow; i++)
                    {
                        var productNo = worksheet.Cells[i, config.ProductNoCol].Value;

                        if (productNo == null) continue;
                        if (productNo != null && string.IsNullOrEmpty(productNo.ToString())) continue;


                        ProductModel product = new ProductModel();
                        product.ProductNo = productNo.ToString();
                        product.Description = worksheet.Cells[i, config.DescriptionCol].Value != null ? worksheet.Cells[i, config.DescriptionCol].Value.ToString() : string.Empty;

                        string priceStr = worksheet.Cells[i, config.PriceCol].Value != null ? worksheet.Cells[i, config.PriceCol].Value.ToString() : string.Empty;
                        decimal.TryParse(priceStr, out decimal price);
                        product.Price = Math.Round(price, 2);

                        products.Add(product);
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing excel file. {ex.Message}");
            }
        }
    }
}
