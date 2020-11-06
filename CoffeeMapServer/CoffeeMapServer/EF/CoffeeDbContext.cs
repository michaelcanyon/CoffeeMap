using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CoffeeMapServer.EF
{
    public class CoffeeDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        
        public DbSet<Roaster> Roasters { get; set; }
        
        public DbSet<Tag> Tags { get; set; }
        
        public DbSet<RoasterTag> RoasterTags { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<RoasterRequest> RoasterRequests { get; set; }

        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoasterTag>()
                .HasKey(rt => new { rt.RoasterId, rt.TagId });
            
            //modelBuilder.Entity<RoasterTag>()
            //    .HasOne(rt => rt.Roaster)
            //    .WithMany(rt => rt.RoasterTags)
            //    .HasForeignKey(rt => rt.RoasterId);

            //modelBuilder.Entity<RoasterTag>()
            //    .HasOne(rt => rt.Tag)
            //    .WithMany(rt => rt.RoasterTags)
            //    .HasForeignKey(rt => rt.TagId);
        }

    }
}
