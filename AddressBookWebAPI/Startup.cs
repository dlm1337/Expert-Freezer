using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AddressBookAPI.Data;
using AddressBookAPI.Repo;
using AddressBookAPI.Service;

namespace AddressBookAPI
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
            // Add configuration
            services.AddSingleton(Configuration);

            // Add repositories
            services.AddScoped<INameAndAddressRepository, NameAndAddressRepository>();

            // Add services
            services.AddScoped<INameAndAddressService, NameAndAddressService>();

            // Add DbContext
            services.AddDbContext<NameAndAddressContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase")));

            // Add other services as needed
            services.AddControllers();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}