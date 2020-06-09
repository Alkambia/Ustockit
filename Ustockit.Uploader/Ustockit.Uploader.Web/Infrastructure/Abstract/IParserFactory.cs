using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ustockit.Uploader.Web.Infrastructure.Abstract
{
    public interface IParserFactory
    {
        IParser GetInstance(string fileType);
    }
}
