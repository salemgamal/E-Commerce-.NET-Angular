using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace API.Models.Data
{
    public class EcommerceDBContextFactory : IDesignTimeDbContextFactory<EcommerceDBContext>
    {
        public EcommerceDBContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EcommerceDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Ecommerce"));

            return new EcommerceDBContext(optionsBuilder.Options);
        }
    }
}