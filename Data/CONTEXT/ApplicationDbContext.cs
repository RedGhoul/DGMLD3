using System;
using System.Collections.Generic;
using System.Text;
using DGMLD3.Data.RDMS;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DGMLD3.Data.CONTEXT
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Graph> Graphs { get; set; }
        public DbSet<PricePlan> PricePlans{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Graph>()
            .HasOne<ApplicationUser>(s => s.Creator)
            .WithMany(g => g.Graphs);

            builder.Entity<Graph>()
            .HasIndex(u => u.Name)
            .IsUnique();


            builder.Entity<PricePlan>()
              .HasMany(c => c.Users)
              .WithOne(e => e.Plan);

            builder.Entity<Graph>()
               .Property(b => b.Nodes)
               .HasColumnType("jsonb");

            builder.Entity<Graph>()
               .Property(b => b.Links)
               .HasColumnType("jsonb");
        }
    }
}
