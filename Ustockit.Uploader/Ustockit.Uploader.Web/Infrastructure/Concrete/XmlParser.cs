using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ustockit.Uploader.Models.Models;
using Ustockit.Uploader.Shared.Util;
using Ustockit.Uploader.Web.Infrastructure.Abstract;
using Ustockit.Uploader.Web.Models;

namespace Ustockit.Uploader.Web.Infrastructure.Concrete
{
    public class XmlParser : IParser
    {
        private IConfigurationRoot _appConfiguration;
        public XmlParser(IConfigurationRoot appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public async Task<IList<ProductModel>> ParseAsync(string file, string manufacturer)
        {
            IList<ProductModel> products = new List<ProductModel>();


            string configPath = $"Configs\\Manufacturer\\{manufacturer}.config.json";
            var path = _appConfiguration["App:ManufacturerConfigPath"];
            if (!string.IsNullOrEmpty(path))
            {
                configPath = string.Format(path, manufacturer);
            }


            var parseConfig = await Task.Run(() => JsonFileUtil.GetFileThenParse<ProductUploadConfig>(configPath));
            var config = parseConfig.XmlConfig;

            foreach (XElement level1Element in XElement.Load(file).Elements(config.HeadTag))
            {
                var productModel = new ProductModel();

                foreach (var item in config.Property)
                {
                    if (level1Element.Elements(item).Any())
                    {
                        if (item == nameof(productModel.ProductNo))
                        {
                            productModel.ProductNo = level1Element.Value;
                        }
                        else if (item == nameof(productModel.Description))
                        {
                            productModel.Description = level1Element.Value;
                        }
                        else if (item == nameof(productModel.Price))
                        {
                            productModel.Description = level1Element.Value;
                        }
                    }
                }
                products.Add(productModel);
            }

            return products;
        }
    }
}
