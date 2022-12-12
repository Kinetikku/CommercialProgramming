using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EntityFramework
{
    internal class CollegeContext : DbContext
    {
        public DbSet<College> Colleges { get; set; }
        public DbSet<Course> Courses { get; set; }

        public CollegeContext(DbContextOptions<CollegeContext> options) : base(options)
        {

        }
    }

    class CollegeContextFactory : IDesignTimeDbContextFactory<CollegeContext>
    {
        public CollegeContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<CollegeContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            return new CollegeContext(optionsBuilder.Options);
        }
    }
}
