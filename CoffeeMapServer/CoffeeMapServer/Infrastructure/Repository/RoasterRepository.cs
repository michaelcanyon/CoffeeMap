using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class RoasterRepository : IRoasterRepository
    {
        private readonly CoffeeDbContext _сontext;

        public RoasterRepository(CoffeeDbContext dbContext)
            => _сontext = dbContext ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(Roaster entity)
            => _сontext.Roasters.Add(entity);

        public void Delete(Roaster entity)
            => _сontext.Roasters.Remove(entity);

        public async Task<Roaster> GetSingleAsync(Guid id,
                                                  [CallerMemberName] string methodName = "")
            => await _сontext.Roasters
               .Include(r => r.OfficeAddress)
               .Include(r=>r.Picture)
               .Include(r => r.RoasterTags)
               .ThenInclude(r => r.Tag)
               .TagWith($"{nameof(RoasterRepository)}.{methodName} ({id})")
               .FirstOrDefaultAsync(e => e.Id == id);

        public void Update(Roaster entity)
            => _сontext.Roasters.Update(entity);

        public async Task<IList<Roaster>> GetListAsync([CallerMemberName] string methodName = "")
            => await _сontext.Roasters
               .Include(r => r.OfficeAddress)
               .OrderBy(r => r.Priority)
               .ThenByDescending(r => r.CreationDate)
               .TagWith($"{nameof(RoasterRepository)}.{methodName}")
               .ToListAsync();

        public async Task<IList<Roaster>> FetchRoastersByAddressIdAsync(Guid id,
                                                                        [CallerMemberName] string methodName = "")
            => await _сontext.Roasters
               .Include(r => r.OfficeAddress)
               .Include(r => r.RoasterTags)
               .ThenInclude(r => r.Tag)
               .Where(r => r.OfficeAddress.Id == id)
               .TagWith($"{nameof(RoasterRepository)}.{methodName} ({id})")
               .ToListAsync();

        public async Task<Roaster> GetRoasterByNameAsync(string roasterName,
                                                         [CallerMemberName] string methodName = "")
            => await _сontext.Roasters
               .Include(r => r.OfficeAddress)
               .Include(r => r.Picture)
               .Include(r => r.RoasterTags)
               .ThenInclude(r => r.Tag)
               .TagWith($"{nameof(RoasterRepository)}.{methodName} ({roasterName})")
               .FirstOrDefaultAsync(e => e.Name == roasterName);

        public async Task<Roaster> GetRoasterByNameNonTrackableAsync(string roasterName,
                                                                     [CallerMemberName] string methodName = "")
            => await _сontext.Roasters
               .AsNoTracking()
               .Include(r => r.OfficeAddress)
               .Include(r => r.Picture)
               .Include(r => r.RoasterTags)
               .ThenInclude(r => r.Tag)
               .TagWith($"{nameof(RoasterRepository)}.{methodName} ({roasterName}) No tracking")
               .FirstOrDefaultAsync(e => e.Name == roasterName);

        public async Task SaveChangesAsync()
            => await _сontext.SaveChangesAsync();
    }
}