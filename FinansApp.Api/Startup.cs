using FinansApp.Api.Helpers;
using FinansApp.Business.Alerts;
using FinansApp.Business.News;
using FinansApp.Business.NewsCategories;
using FinansApp.Business.Portfoy;
using FinansApp.Business.SignalRequests;
using FinansApp.Business.Signals;
using FinansApp.Business.StaticPages;
using FinansApp.Business.Users;
using FinansApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansApp.Api
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
            // services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<ISignalService, SignalService>();
            services.AddScoped<IStaticPageService, StaticPageService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPortfoyService, PortfoyService>();
            services.AddScoped<INewsCategoryService, NewsCategoryService>();
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<ISignalRequestService, SignalRequestService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinansApp.Api", Version = "v1" });
            });
            services.AddDbContext<FinansAppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinansApp.Api v1"));
            // }
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinansApp.Api v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            //app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            // app.UseAuthentication();
            // app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
