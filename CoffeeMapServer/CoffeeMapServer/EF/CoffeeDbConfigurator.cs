using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.EF
{
    public partial class CoffeeDbContext
    {
        private const string nonestr = "none";
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                    
            modelBuilder.Entity<Address>()
                .Property(p => p.AddressStr)
                .IsRequired();
            modelBuilder.Entity<Address>()
                .Property(p => p.OpeningHours)
                .IsRequired()
                .HasDefaultValue(nonestr);

            modelBuilder.Entity<Roaster>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Roaster>()
                .Property(p => p.Description)
                .HasDefaultValue(nonestr);
            modelBuilder.Entity<Roaster>()
                .Property(p => p.ContactEmail)
                .HasDefaultValue(nonestr);
            modelBuilder.Entity<Roaster>()
                .Property(p => p.ContactNumber)
                .IsRequired();
            modelBuilder.Entity<Roaster>()
                .Property(p => p.TelegramProfileLink)
                .HasDefaultValue(nonestr);
            modelBuilder.Entity<Roaster>()
                .Property(p => p.VkProfileLink)
                .HasDefaultValue(nonestr);
            modelBuilder.Entity<Roaster>()
                .Property(p => p.WebSiteLink)
                .HasDefaultValue(nonestr);

            modelBuilder.Entity<Roaster>()
                .HasOne(r => r.OfficeAddress)
                .WithMany(a => a.Roasters);

            modelBuilder.Entity<RoasterRequest>()
                .Property(p => p.TagString)
                .HasDefaultValue(nonestr);

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
            modelBuilder.Entity<RoasterTag>()
                .HasOne(r => r.Roaster)
                .WithMany(e => e.RoasterTags)
                .HasForeignKey(r => r.RoasterId);
            modelBuilder.Entity<RoasterTag>()
                .HasOne(t => t.Tag)
                .WithMany(e => e.RoasterTags)
                .HasForeignKey(t => t.TagId);


        }
    }
}