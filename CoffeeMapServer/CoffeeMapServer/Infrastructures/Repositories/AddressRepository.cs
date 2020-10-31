using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class AddressRepository : IAddessRepository
    {
        CoffeeDbContext Context { get; set; }

        public AddressRepository(CoffeeDbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task Create(Address entity)
        {
            await Context.Addresses.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Context.Addresses.Remove((await Context.Addresses.Where(node => node.Id == id).ToListAsync()).First());
            await Context.SaveChangesAsync();
        }

        public async Task<Address> GetSingle(Guid id)
        {
            var addresses = await Context.Addresses.Where(node => node.Id == id).ToListAsync();
            return addresses.Count() > 0 ? addresses.First() : null;
        }

        public async Task Update(Address entity)
        {
            Context.Addresses.Update(entity);
            await Context.SaveChangesAsync();

        }

        public async Task<List<Address>> GetList()
        {
            var addresses = await Context.Addresses.ToListAsync();
            return addresses.Count() > 0 ? addresses : null;
        }

        public async Task<Address> GetSingle(Address entity)
        {
            var addrs = await Context.Addresses.Where(node => node.AddressStr.Equals(entity.AddressStr)).ToListAsync();
            return addrs.Count() > 0 ? addrs.First() : null;
        }
    }
}
