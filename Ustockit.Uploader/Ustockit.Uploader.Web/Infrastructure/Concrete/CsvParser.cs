using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using Ustockit.Uploader.Models.Models;
using Ustockit.Uploader.Shared.Util;
using Ustockit.Uploader.Web.Infrastructure.Abstract;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Concrete
{
    public class CsvParser : IParser
    {
        private IConfigurationRoot _appConfiguration;
        public CsvParser(IConfigurationRoot appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }


        public async Task<IList<ProductModel>> ParseAsync(string filePath, string manufacturer)
        {
            try
            {
                var products = new List<ProductModel>();
                string configPath = $"Configs\\Manufacturer\\{manufacturer}.config.json";
                var path = _appConfiguration["App:ManufacturerConfigPath"];
                if (!string.IsNullOrEmpty(path))
                {
                    configPath = string.Format(path, manufacturer);
                }

                var parseConfig = await Task.Run(() => JsonFileUtil.GetFileThenParse<ProductUploadConfig>(configPath));
                var config = parseConfig.CsvConfig;

                FileInfo file = new FileInfo(filePath);

                CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
                var csvParser = new CsvParser<ProductModel>(csvParserOptions, new ProductCsvMappings(config));
                var records = csvParser.ReadFromStream(file.OpenRead(), Encoding.ASCII).ToList();

                foreach (var row in records)
                {
                    int rowNumber = row.RowIndex + 1;

                    if (!row.IsValid)
                        throw new Exception($"There is an error parsing the value at row ({rowNumber}). Additional Info: {row.Error.Value}");

                    products.Add(row.Result);
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing csv file. {ex.Message}");
            }
        }
    }
}
