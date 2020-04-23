using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DGMLD3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Graph> Graphs { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<PricePlan> PricePlans{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Graph>()
            .HasIndex(u => u.Name)
            .IsUnique();


            builder.Entity<PricePlan>()
              .HasMany(c => c.Users)
              .WithOne(e => e.Plan);
        }
    }
}
