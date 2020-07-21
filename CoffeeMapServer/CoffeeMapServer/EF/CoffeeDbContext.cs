using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.EF
{
    public class CoffeeDbContext : DbContext
    {
        public DbSet<CoffeeSpot> CoffeeSpots { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CoIndex> CoIndexes { get; set; }
        public DbSet<Roaster> Roasters { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Coffee_Address> Coffee_Addresses { get; set; }
        public DbSet<Coffee_Index> coffee_Indices { get; set; }
        public DbSet<Coffee_Roaster> Coffee_Roasters { get; set; }
        public DbSet<Coffee_Tag> coffee_Tags { get; set; }
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
