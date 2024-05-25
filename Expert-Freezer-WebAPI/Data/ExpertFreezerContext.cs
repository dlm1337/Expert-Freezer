using Microsoft.EntityFrameworkCore;
using ExpertFreezerAPI.Models;

namespace ExpertFreezerAPI.Data
{
    public class ExpertFreezerContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ExpertFreezerContext(DbContextOptions<ExpertFreezerContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ExpertFreezerProfile> expertFreezerProfiles { get; set; }
    }
}