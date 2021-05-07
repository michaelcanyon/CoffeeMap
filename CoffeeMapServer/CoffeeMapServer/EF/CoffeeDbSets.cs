using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.EF
{
    public partial class CoffeeDbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Roaster> Roasters { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<PictureRequest> PictureRequests { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<RoasterTag> RoasterTags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RoasterRequest> RoasterRequests { get; set; }
    }
}