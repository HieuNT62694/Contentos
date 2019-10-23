﻿
using BatchjobService.Entities;
using BatchjobService.HangFireService;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BatchjobService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add Dbcontext
            services.AddDbContext<ContentoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ContentoDb"));
            });
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDb")));
            services.AddHangfireServer();
            services.AddScoped<IUpdateStatusService, UpdateStatusService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Add timer service
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHangfireDashboard();
            //RecurringJob.AddOrUpdate("UpdateStatusDeadline", () => service.UpdateStatus(), "0 10 * * *", TimeZoneInfo.Utc);
            RecurringJob.AddOrUpdate<IUpdateStatusService>("UpdateStatusDeadline", context => context.UpdateStatus(), "* 17 * * *", TimeZoneInfo.Utc);
            //RecurringJob.AddOrUpdate<IUpdateStatusService>("UpdateStatusDeadline", context => context.UpdateStatus(), Cron.Daily,TimeZoneInfo.Utc );
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
