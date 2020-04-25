using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesc.IntegrationTests
{
    public class TestApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                          .UseStartup<TEntryPoint>()
                          .UseSolutionRelativeContentRoot("AgileWorksServiceDesk")
                          .ConfigureAppConfiguration((context, conf) =>
                          {
                              var projectDir = Directory.GetCurrentDirectory();
                              var configPath = Path.Combine(projectDir, "appsettings.json");

                              conf.AddJsonFile(configPath);
                          });
        }
    }
}
