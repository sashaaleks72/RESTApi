using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Net7.WebApi.Test.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            var dbOptionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            dbOptionBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            var options = dbOptionBuilder.Options;

            return new ApplicationDbContext(options);
        }
    }
}
