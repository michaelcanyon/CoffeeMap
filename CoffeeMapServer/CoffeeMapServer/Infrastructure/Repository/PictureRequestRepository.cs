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
    public class PictureRequestRepository : IPictureRequestRepository
    {
        private readonly CoffeeDbContext _coffeeDbContext;

        public PictureRequestRepository(CoffeeDbContext coffeeDbContext)
            => _coffeeDbContext = coffeeDbContext;

        public void Add(PictureRequest entity)
            => _coffeeDbContext.Add(entity);

        public void Delete(PictureRequest picture)
            => _coffeeDbContext.Remove(picture);

        public async Task<IList<PictureRequest>> GetListAsync([CallerMemberName] string methodName = "")
            => await
            _coffeeDbContext.PictureRequests
            .TagWith($"{nameof(PictureRequestRepository)}.{methodName}")
            .ToListAsync();

        public async Task<PictureRequest> GetPictureReqByRoasterReqIdAsyncAsNoTracking(Guid roasterId)
            => await _coffeeDbContext.PictureRequests
                                     .Where(rr => rr.RoasterRequestId == roasterId)
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync();
        public async Task<PictureRequest> GetSingleAsync(Guid id,
                                                  [CallerMemberName] string methodName = "")
            => await _coffeeDbContext.PictureRequests
                     .Where(p => p.Id == id)
                     .TagWith($"{nameof(PictureRequestRepository)}.{methodName} ({id})")
                     .FirstOrDefaultAsync();

        public async Task SaveChangesAsync()
            => await _coffeeDbContext.SaveChangesAsync();

        public void Update(PictureRequest entity)
            => _coffeeDbContext.Update(entity);
    }
}
