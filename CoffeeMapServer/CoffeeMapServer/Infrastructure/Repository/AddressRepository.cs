using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        CoffeeDbContext Context { get; set; }

        public AddressRepository(CoffeeDbContext dbContext)
            => Context = dbContext;

        public void Add(Address entity)
            => Context.Addresses.Add(entity);

        public void Delete(Address entity)
            => Context.Addresses.Remove(entity);

        public async Task<Address> GetSingleAsync(Guid id)
            => await Context.Addresses.FirstOrDefaultAsync(e => e.Id == id);

        public void Update(Address entity)
            => Context.Addresses.Update(entity);

        public async Task<IList<Address>> GetListAsync()
            => await Context.Addresses.ToListAsync();

        public async Task<Address> GetSingleAsync(Address entity)
            => await Context.Addresses.FirstOrDefaultAsync(node => node.AddressStr.Equals(entity.AddressStr));

        public async Task SaveChangesAsync()
        => await Context.SaveChangesAsync();
    }
}
