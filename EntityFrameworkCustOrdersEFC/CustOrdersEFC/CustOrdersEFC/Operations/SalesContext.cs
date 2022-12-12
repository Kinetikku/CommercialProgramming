using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustOrdersEFC.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CustOrdersEFC.Operations
{
    internal class SalesContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }
    }
}
