using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructure.Interface;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructure.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private readonly CoffeeDbContext _coffeeDbContext;

        public PictureRepository(CoffeeDbContext coffeeDbContext)
            => _coffeeDbContext = coffeeDbContext;

        public void Add(Picture entity)
            => _coffeeDbContext.Add(entity);

        public void Delete(Picture picture)
            => _coffeeDbContext.Remove(picture);

        public async Task<IList<Picture>> GetListAsync([CallerMemberName] string methodName = "")
            => await
            _coffeeDbContext.Pictures
            .TagWith($"{nameof(PictureRepository)}.{methodName}")
            .ToListAsync();
        public async Task<Picture> GetPictureByRoasterIdAsyncAsNoTracking(Guid roasterId)
            => await _coffeeDbContext.Pictures
                                     .Where(p => p.RoasterId == roasterId)
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync();

        public async Task<Picture> GetSingleAsync(Guid id,
                                                  [CallerMemberName] string methodName = "")
            => await _coffeeDbContext.Pictures
                     .Where(p => p.Id == id)
                     .TagWith($"{nameof(PictureRepository)}.{methodName} ({id})")
                     .FirstOrDefaultAsync();

        public async Task SaveChangesAsync()
            => await _coffeeDbContext.SaveChangesAsync();

        public void Update(Picture entity)
            => _coffeeDbContext.Update(entity);
    }
}