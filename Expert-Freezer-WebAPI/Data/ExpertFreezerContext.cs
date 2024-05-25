using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            // Connect to PostgreSQL with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> users { get; set; }
        public DbSet<ExpertFreezerProfile> expertFreezerProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define primary keys
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<ExpertFreezerProfile>()
                .HasKey(e => e.Id);

            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure the one-to-one relationship
            modelBuilder.Entity<ExpertFreezerProfile>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<ExpertFreezerProfile>(e => e.Username)
                .HasPrincipalKey<User>(u => u.Username)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure Username is unique in ExpertFreezerProfile
            modelBuilder.Entity<ExpertFreezerProfile>()
                .HasIndex(e => e.Username)
                .IsUnique();
        }
    }
}