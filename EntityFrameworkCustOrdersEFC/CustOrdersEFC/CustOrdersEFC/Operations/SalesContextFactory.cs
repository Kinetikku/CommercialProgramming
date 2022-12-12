using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustOrdersEFC.Operations
{
    internal class SalesContextFactory : IDesignTimeDbContextFactory<SalesContext>
    {
        public SalesContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new SalesContext(optionsBuilder.Options);
        }
    }
}
