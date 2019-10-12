using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampaignService.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.Generation.Processors.Security;
using NSwag;
using MediatR;
using FluentValidation.AspNetCore;
using CampaignService.Application.Queries;
using CampaignService.Application.Queries.GetCampaign;
using Steeltoe.Discovery.Client;
using CampaignService.Application.Commands.CreateCampaign;

namespace CampaignService
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
            //MediatR and flutentvalidator
            services.AddMediatR(typeof(GetCampaignRequest).Assembly);
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetCampaignValidator>());
            //services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCampaignCommand>());
            //add ueraka
            services.AddDiscoveryClient(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            //addd cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //add swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseDiscoveryClient();
            app.UseMvc();
        }
    }
}
