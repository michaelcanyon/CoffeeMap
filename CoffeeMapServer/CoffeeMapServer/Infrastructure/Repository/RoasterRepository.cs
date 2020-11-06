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
    public class RoasterRepository : IRoasterRepository
    {
        private readonly CoffeeDbContext _сontext;

        public RoasterRepository(CoffeeDbContext dbContext)
            => _сontext = dbContext;

        public void Add(Roaster entity)
            => _сontext.Roasters.Add(entity);

        public void Delete(Roaster entity)
            => _сontext.Roasters.Remove(entity);

        public async Task<Roaster> GetSingleAsync(Guid id)
            => await _сontext.Roasters.FirstOrDefaultAsync(e => e.Id == id);

        public void Update(Roaster entity)
            => _сontext.Roasters.Update(entity);

        public async Task<IList<Roaster>> GetListAsync()
            => await _сontext.Roasters.ToListAsync();

        public async Task<Roaster> GetSingleByAddressIdAsync(Guid addressId)
            => await _сontext.Roasters.FirstOrDefaultAsync(e => e.OfficeAddressId == addressId);

        public async Task<IList<Roaster>> FetchRoastersByAddressIdAsync(Guid id)
            => await _сontext.Roasters.Where(r => r.OfficeAddressId == id).ToListAsync();

        public async Task<Roaster> GetRoasterAsync(Roaster entity)
        {
            return await _сontext.Roasters.Where(node => node.Name == entity.Name &&
                               node.OfficeAddressId == entity.OfficeAddressId &&
                               node.ContactEmail == entity.ContactEmail &&
                               node.ContactNumber == entity.ContactNumber &&
                               node.WebSiteLink == entity.WebSiteLink &&
                               node.InstagramProfileLink == entity.InstagramProfileLink &&
                               node.VkProfileLink == entity.VkProfileLink &&
                               node.TelegramProfileLink == entity.TelegramProfileLink)
                .FirstOrDefaultAsync();
        }

        public async Task<Roaster> GetRoasterByNameAsync(string roasterName)
            => await _сontext.Roasters.FirstOrDefaultAsync(e => e.Name == roasterName);

        public async Task SaveChangesAsync()
            => await _сontext.SaveChangesAsync();
    }
}