using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ustockit.Uploader.Configuration;

namespace Ustockit.Uploader.Web.Infrastructure.Ext
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }
    }
}