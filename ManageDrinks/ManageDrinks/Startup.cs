using ManageDrinks.Data;
using ManageDrinks.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;


namespace ManageDrinks
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ManageDrinksDbContext>(opts =>
            {
                opts.UseSqlite($"Data Source={env.ContentRootPath}/ManageDrinks.db;", dbOpts => { });
            });

            services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddScoped<IBarRepository, BarRepository>();
            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IBreweryBeersRepository, BreweryBeersRepository>();
            services.AddScoped<IBarBeersRepository, BarBeersRepository>();


            services.AddCors(opts => { });
            services.AddResponseCaching(opts =>
            {
                opts.SizeLimit = 4096;
            });
            services.AddResponseCompression(opts => { });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ManageDrinks", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManageDrinks v1"));

            }

            // app.UseHttpsRedirection();

            app.UseCors(opts =>
            {
                opts.AllowAnyOrigin();
            });
            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
