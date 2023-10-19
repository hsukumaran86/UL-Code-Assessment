using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using Serilog;
using UL.Application;
using UL.Infrastructure.Cache;

namespace UL.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 32_768; // Set your desired limit in bytes (e.g., 32KB)
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Math Expression Evaluator", Version = "v1" });
            });

            services.AddMemoryCache();

            services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 12;
                    options.Window = TimeSpan.FromSeconds(5);
                    options.QueueLimit = 0;

                }));


            services.AddApiVersioning(options =>
            {
                // Specify the default API version.
                options.DefaultApiVersion = new ApiVersion(1, 0);
                // If a client doesn't specify an API version, this will be used.
                options.AssumeDefaultVersionWhenUnspecified = true;
                // Specify supported API versions.
                options.ReportApiVersions = true;
            });

            // Configure Serilog with file logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

            services.AddControllers();

            services.AddScoped<IMathService, MathService>();
            services.AddSingleton<ICacheService, MemoryCacheService>();



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Math Expression Evaluator v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            // Rate limiting middleware
            app.UseRateLimiter();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseApiVersioning();

            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            


        }
    }
}
