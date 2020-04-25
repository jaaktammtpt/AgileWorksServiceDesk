using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileWorksServiceDesk;
using AgileWorksServiceDesk.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgileWorksServiceDesc.IntegrationTests
{
    public class FakeStartup : Startup
    {
        
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (dbContext == null)
            {
                throw new NullReferenceException("Cannot get instance of dbContext");
            }

            if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("mydb.db"))
            {
                throw new Exception("LIVE SETTINGS IN TESTS!");
            }            

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            Utilities.InitializeDbForTests(dbContext);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddAntiforgery(t =>
            {
                t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
            });
        }

    }
}
