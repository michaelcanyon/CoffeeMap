using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Roaster> GetSingleAsync(Guid id)
            => await _сontext.Roasters.Include(r=>r.OfficeAddress).Include(r=>r.RoasterTags).ThenInclude(r=>r.Tag).FirstOrDefaultAsync(e => e.Id == id);

        public void Update(Roaster entity)
            => _сontext.Roasters.Update(entity);

        public async Task<IList<Roaster>> GetListAsync()
            => await _сontext.Roasters.Include(r => r.OfficeAddress).Include(r=>r.RoasterTags).ThenInclude(r=>r.Tag).ToListAsync();

        public async Task<IList<Roaster>> FetchRoastersByAddressIdAsync(Guid id)
            => await _сontext.Roasters.Include(r => r.OfficeAddress).Include(r=>r.RoasterTags).ThenInclude(r=>r.Tag).Where(r => r.OfficeAddress.Id == id).ToListAsync();

        public async Task<Roaster> GetRoasterByNameAsync(string roasterName)
            => await _сontext.Roasters.Include(r => r.OfficeAddress).Include(r => r.RoasterTags).ThenInclude(r => r.Tag).Include(r => r.OfficeAddress).FirstOrDefaultAsync(e => e.Name == roasterName);

        public async Task SaveChangesAsync()
            => await _сontext.SaveChangesAsync();
    }
}