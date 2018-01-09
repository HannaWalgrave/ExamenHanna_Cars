using System;
using ExamenHanna_Cars.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenHanna_Cars.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(b => b.Id);

            modelBuilder.Entity<Car>().HasMany(b => b.Owner).WithOne(ba => ba.Car);
            modelBuilder.Entity<Car>().HasOne(b => b.Brand).WithMany(g => g.Cars);

            modelBuilder.Entity<CarOwner>().HasKey(ab => new { ab.OwnerId, ab.CarId });

            modelBuilder.Entity<Owner>().HasKey(a => a.Id);

            modelBuilder.Entity<Owner>().HasMany(b => b.Cars).WithOne(ba => ba.Owner);

            modelBuilder.Entity<Brand>().HasKey(b => b.Id);

        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brand { get; set; }

    }
}