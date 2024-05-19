using Microsoft.EntityFrameworkCore;
using AddressBookAPI.Models;

namespace AddressBookAPI.Data
{
    public class NameAndAddressContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public NameAndAddressContext(DbContextOptions<NameAndAddressContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            NameAndAddresses = Set<NameAndAddress>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<NameAndAddress> NameAndAddresses { get; set; }
    }
}