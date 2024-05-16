using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Entiry;

namespace RealEstate.Reopsitory.Configuration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasOne(Prop => Prop.Category).WithMany().HasForeignKey(p => p.CategoryId);
            builder.Property(p => p.Price).HasColumnType("Money");
        }
    }
}
