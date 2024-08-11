using CarOperationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Model)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
