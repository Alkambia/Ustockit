using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ustockit.Uploader.Web.Models
{
    public class BinaryObject
    {
        public Guid Id { get; set; }
        public byte[] Bytes { get; set; }
        public BinaryObject(byte[] bytes)
        {
            Id = Guid.NewGuid();
            Bytes = bytes;
        }
    }
}
