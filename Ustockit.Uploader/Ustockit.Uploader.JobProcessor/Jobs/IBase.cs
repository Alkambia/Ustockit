using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ustockit.Uploader.JobProcessor.Jobs
{
    public interface IBase
    {
        Task Execute<T>(T args);
    }
}
