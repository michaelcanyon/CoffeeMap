using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.EF
{
    public partial class CoffeeDbContext
    {
        //TODO: don't forget to fix nullable fields in argument entitites
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(p => p.AddressStr)
                .IsRequired()
                .HasDefaultValue("");
            modelBuilder.Entity<Address>()
                .Property(p => p.OpeningHours)
                .IsRequired()
                .HasDefaultValue("");

            modelBuilder.Entity<Roaster>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Roaster>()
                .Property(p => p.Description)
                .HasDefaultValue("Description will apperar as soon as possible:)");
            modelBuilder.Entity<Roaster>()
                .Property(p => p.ContactEmail)
                .IsRequired();
            modelBuilder.Entity<Roaster>()
                .Property(p => p.ContactNumber)
                .IsRequired();
            modelBuilder.Entity<Roaster>()
                .Property(p => p.OfficeAddressId)
                .HasDefaultValue(null);
            modelBuilder.Entity<Roaster>()
                .Property(p => p.OfficeAddress)
                .HasDefaultValue(null);
            modelBuilder.Entity<Roaster>()
                .Property(p => p.InstagramProfileLink)
                .HasDefaultValue("");
            modelBuilder.Entity<Roaster>()
                .Property(p => p.TelegramProfileLink)
                .HasDefaultValue("");
            modelBuilder.Entity<Roaster>()
                .Property(p => p.VkProfileLink)
                .HasDefaultValue("");
            modelBuilder.Entity<Roaster>()
                .Property(p => p.WebSiteLink)
                .HasDefaultValue("");

            
            modelBuilder.Entity<RoasterRequest>()
                .Property(p => p.Address)
                .IsRequired();
            modelBuilder.Entity<RoasterRequest>()
                .Property(p => p.Roaster)
                .IsRequired();
            modelBuilder.Entity<RoasterRequest>()
                .Property(p => p.TagString)
                .HasDefaultValue("");

            modelBuilder.Entity<Tag>()
                .Property(p => p.TagTitle)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Login)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Password)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Role)
                .IsRequired();


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