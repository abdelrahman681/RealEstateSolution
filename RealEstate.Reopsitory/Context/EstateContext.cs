using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.Entiry.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Context
{
    public class EstateContext : IdentityDbContext<ApplicationUser>
    {
        public EstateContext(DbContextOptions<EstateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
