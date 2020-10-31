using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PF.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<PensionModifier> DisabilityGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Experience>()
                .HasKey(exp => new { exp.PersonId, exp.PositionId });
            builder.Entity<Experience>()
                .HasOne(exp => exp.Person)
                .WithMany(p => p.Experiences)
                .HasForeignKey(exp => exp.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PensionModifier>()
                .HasMany(dg => dg.People)
                .WithOne(p => p.Modifier)
                .HasForeignKey(p => p.ModifierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
