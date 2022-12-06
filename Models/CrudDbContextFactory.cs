using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiChallenge.Models
{
    public class CrudDbContextFactory : IDesignTimeDbContextFactory<TheaterDbContext>
    {
        public TheaterDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            // Here we create the DbContextOptionsBuilder manually.        
            var builder = new DbContextOptionsBuilder<TheaterDbContext>();

            // Build connection string. This requires that you have a connectionstring in the appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            // Create our DbContext.
            return new TheaterDbContext(builder.Options);
        }
    }
}
