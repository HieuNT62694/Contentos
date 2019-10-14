using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.Generation.Processors.Security;
using MediatR;
using Steeltoe.Discovery.Client;
using FluentValidation.AspNetCore;
using ContentProccessService.Entities;

namespace ContentProccessService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //add Dbcontext
            services.AddDbContext<ContentoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CampaignDb"));
            });

            //add swagger
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = string.Format($"Campaign Service");
                    document.Info.Description = string.Format($"Developer Documentation Page For Campaign Service");
                };
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("Jwt Token Authentication", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Using: Bearer + your jwt token",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));
            });

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddDiscoveryClient(Configuration);
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
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseDiscoveryClient();
            app.UseMvc();
        }
    }
}
