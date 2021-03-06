using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EmployNetTools.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace EmployNetTools
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
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddDbContext<DataSurfContext>(options =>
            {

                options.UseSqlServer(Configuration["ConnectionString"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
                });
            });

            // azure has its own CORS configuration
            //services.AddCors(options => options.AddPolicy("AllowMyApp", policy => policy.AllowAnyOrigin()));
            //services.AddCors(Options => Options.AddPolicy(name: "MyAllowSpecificOrigins",
            //                  builder =>
            //                  {
            //                      //builder.WithOrigins("https://testing.clinistic.com",
            //                      //                    "https://jsonformatter.curiousconcept.com",
            //                      //                    "https://linkyounow.com")
            //                      builder.AllowAnyHeader()
            //                      .AllowAnyMethod()
            //                      .AllowCredentials()
            //                      .SetIsOriginAllowed(orgin => true)
            //                      .AllowAnyOrigin();

            //                  }));

            services.AddControllersWithViews().
                  AddJsonOptions(options =>
                  {
                      options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                      options.JsonSerializerOptions.PropertyNamingPolicy = null;
                  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUi3();
            }

           // app.UseCors("MyAllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
            });

        }
    }
}
