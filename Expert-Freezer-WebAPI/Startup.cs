using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpertFreezerAPI.Data;
using ExpertFreezerAPI.Repo;
using ExpertFreezerAPI.Service;
using Microsoft.AspNetCore.Identity;
using ExpertFreezerAPI.Models;

namespace ExpertFreezerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add configuration
            services.AddSingleton(Configuration);

            // Add repositories
            services.AddScoped<IExpertFreezerRepository, ExpertFreezerRepository>();

            // Add services
            services.AddScoped<IExpertFreezerService, ExpertFreezerService>();
            services.AddScoped<ITokenService, TokenService>();

            // Register the IPasswordHasher<User> service
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Add DbContext
            services.AddDbContext<ExpertFreezerContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase")));

            // Add JWT authentication with succesful login
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddSwaggerGen();
            // Add other services as needed
            services.AddControllers();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Ensure this line is added to enable authentication
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}