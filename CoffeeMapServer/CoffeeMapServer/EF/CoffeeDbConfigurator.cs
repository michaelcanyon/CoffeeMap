using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.EF
{
    public partial class CoffeeDbContext
    {
        //TODO: don't forget to fix nullable fields in argument entitites
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoasterTag>()
                .HasKey(rt => new { rt.RoasterId, rt.TagId });

            modelBuilder.Entity<Roaster>()
                .HasOne(e => e.OfficeAddress)
                .WithOne()
                .HasForeignKey<Roaster>(e => e.OfficeAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Address>()
            //    .HasKey(e => e.Id);

            //modelBuilder.Entity<Roaster>()
            //    .HasKey(e => e.Id);

            //modelBuilder.Entity<Tag>()
            //    .HasKey(e => e.Id);

            //modelBuilder.Entity<RoasterRequest>()
            //    .HasKey(e => e.Id);

            //modelBuilder.Entity<User>()
            //    .HasKey(e => e.Id);

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