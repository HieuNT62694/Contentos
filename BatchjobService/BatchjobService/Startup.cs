
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
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Linq;
using MediatR;
using Steeltoe.Discovery.Client;
using BatchjobService.Application.Recommandation;

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
            services.AddDbContext<ContentoDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ContentoDb"));
            });
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDb")));
            services.AddHangfireServer();
            services.AddScoped<IUpdateStatusService, UpdateStatusService>();
            services.AddScoped<IPublishFBService, PublishFB>();
            services.AddScoped<IUpdateBeforePublishingService, UpdateBeforePublishingService>();
            //and scoped
            services.AddScoped<IModel, Model>();
            services.AddScoped<IRun, Run>();
            services.AddScoped<IAlgorithmn, Algorithmn>();
            services.AddScoped<IAlgorithmDataLogic, AlgorithmDataLogic>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddDiscoveryClient(Configuration);
            //Add timer service

            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = string.Format($"Publish Service");
                    document.Info.Description = string.Format($"Developer Documentation Page For Publish Service");
                };
                config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Using: Bearer + your jwt token"
                });

                config.OperationProcessors.Add(
                        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                //config.DocumentProcessors.Add(new SecurityDefinitionAppender("Jwt Token Authentication", new OpenApiSecurityScheme
                //{
                //    Type = OpenApiSecuritySchemeType.ApiKey,
                //    Name = "Authorization",
                //    Description = "Using: Bearer + your jwt token",
                //    In = OpenApiSecurityApiKeyLocation.Header
                //}));
            });
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
            RecurringJob.AddOrUpdate<IRun>("UpdateStatusDeadline", context => context.Handle(), "* 17 * * *", TimeZoneInfo.Utc);
            //RecurringJob.AddOrUpdate<IUpdateStatusService>("UpdateStatusDeadline", context => context.UpdateStatus(), Cron.Daily,TimeZoneInfo.Utc );
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseDiscoveryClient();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
