using System;
using System.Collections.Generic;
using System.Text;

namespace Ustockit.Uploader.Shared.Models
{
    public class StoredFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Directory { get; set; }
    }
}
