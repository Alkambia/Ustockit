using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ustockit.Uploader.Shared.Util
{
    public static class JsonFileUtil
    {
        public static T GetFileThenParse<T>(string filePath) where T : class
        {
            try
            {
                string jsonFileData;
                using (StreamReader r = new StreamReader(filePath))
                {
                    jsonFileData = r.ReadToEnd();
                }

                var fileData = JsonConvert.DeserializeObject<T>(jsonFileData);

                return (T)fileData;
            }
            catch (Exception)
            {

                throw new Exception($"Error getting jason file data.");
            }
        }
    }
}
