using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructure;
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

        public async Task<Address> GetSingleAsync(Guid id,
                                                  [CallerMemberName] string methodName = "")
            => await _context.Addresses
               .TagWith($"{nameof(AddressRepository)}.{methodName} ({id})")
               .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Address> GetSingleAsync(string addressStr,
                                                  [CallerMemberName] string methodName = "")
            => await _context.Addresses
               .TagWith($"{nameof(AddressRepository)}.{methodName} ({addressStr})")
               .FirstOrDefaultAsync(e => e.AddressStr == addressStr);

        public async Task<Address> GetSingleAsNoTrackingAsync(string addressStr,
                                                              [CallerMemberName] string methodName = "")
            => await _context.Addresses
               .AsNoTracking()
               .TagWith($"{nameof(AddressRepository)}.{methodName} ({addressStr}) No Tracking")
               .FirstOrDefaultAsync(e => e.AddressStr == addressStr);

        public void Update(Address entity)
            => _context.Addresses.Update(entity);

        //TODO: fix this
        public async Task<IList<Address>> GetListAsync([CallerMemberName] string methodName = "")
            => await _context.Addresses
               .Include(a => a.Roasters)
               .CustomTagWith()
               .ToListAsync();

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}