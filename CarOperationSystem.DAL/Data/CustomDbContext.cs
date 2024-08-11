using CarOperationSystem.DAL.Configuration;
using CarOperationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Data
{
    public class CustomDbContext:DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
