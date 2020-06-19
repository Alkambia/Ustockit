using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ustockit.Uploader.JobProcessor.Jobs;
using Ustockit.Uploader.Shared.Util;
using Ustockit.Uploader.Web.Infrastructure.Concrete;
using Ustockit.Uploader.Web.Infrastructure.Ext;

namespace Ustockit.Uploader.Web
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            if (bool.Parse(_appConfiguration["App:Hangfire:IsServerEnabled"]))
            {
                var connectionStringName = "Default";
                try
                {
                    connectionStringName = _appConfiguration["App:Hangfire:ConnectionString"];
                    if (String.IsNullOrEmpty(connectionStringName))
                    {
                        connectionStringName = "Default";
                    }
                }
                catch (Exception)
                {
                    //supress & fall-back on "Default"
                    //TODO : log exception to logger
                    connectionStringName = "Default";
                }
                // Add Hangfire services.
                services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseStorage(
                    new MySqlStorage(_appConfiguration.GetConnectionString(connectionStringName), new MySqlStorageOptions {
                        TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DashboardJobListLimit = 50000,
                        TransactionTimeout = TimeSpan.FromMinutes(1),
                        TablePrefix = "Hangfire"
                    })
                    //.UseSqlServerStorage(_appConfiguration.GetConnectionString(connectionStringName), new SqlServerStorageOptions
                    //{
                    //    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    //    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    //    QueuePollInterval = TimeSpan.Zero,
                    //    UseRecommendedIsolationLevel = true,
                    //    UsePageLocksOnDequeue = true,
                    //    DisableGlobalLocks = true
                    //}
                    ));

                // Add the processing server as IHostedService
                services.AddHangfireServer();
            }

            services.AddTransient<IStoreFile, StoreFile>();
            services.AddTransient<IProcessFileStored, ProcessFileStored>();
            services.AddTransient<IProcessBatchFile, ProcessBatchFile>();
            services.AddTransient<IProcessProduct, ProcessProduct>();
            services.AddTransient(o => {
                return _env.GetAppConfiguration();
            });

            services.AddTransient<ExcelParser>();
            services.AddTransient<CsvParser>();
            services.AddTransient<XmlParser>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            if (bool.Parse(_appConfiguration["App:Hangfire:IsDashboardEnabled"]))
            {
                //todo: Authorization Filter will be added later
                app.UseHangfireDashboard();
            }
            if (bool.Parse(_appConfiguration["App:Hangfire:IsServerEnabled"]))
            {
                //todo: Implement Queue priority when more than one job
                //todo: Domain Service can also be implemented if highest priority wont work.
                app.UseHangfireServer();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
