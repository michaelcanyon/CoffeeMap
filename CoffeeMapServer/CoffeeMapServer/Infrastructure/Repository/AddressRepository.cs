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
        private readonly CoffeeDbContext _context;

        public AddressRepository(CoffeeDbContext dbContext)
            => _context = dbContext ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(Address entity)
            => _context.Addresses.Add(entity);

        public void Delete(Address entity)
            => _context.Addresses.Remove(entity);

        public async Task<Address> GetSingleAsync(Guid id)
            => await _context.Addresses.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Address> GetSingleAsync(string addressStr)
            => await _context.Addresses.FirstOrDefaultAsync(e => e.AddressStr == addressStr);

        public async Task<Address> GetSingleAsNoTrackingAsync(string addressStr)
            => await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(e => e.AddressStr == addressStr);

        public void Update(Address entity)
            => _context.Addresses.Update(entity);

        public async Task<IList<Address>> GetListAsync()
            => await _context.Addresses.ToListAsync();

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}