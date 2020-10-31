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
        CoffeeDbContext Context { get; set; }

        public RoasterRepository(CoffeeDbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task Create(Roaster entity)
        {
            try
            {
                await Context.Roasters.AddAsync(entity);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Roaster repository create method failed to complete");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var roaster = (await Context.Roasters.Where(node => node.Id.Equals(id)).ToListAsync()).First();
                Context.Roasters.Remove(roaster);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Roaster repository delete method failed to complete");
            }
        }

        public async Task<Roaster> GetSingle(Guid id)
        {
            try
            {
                var roasters = await Context.Roasters.Where(node => node.Id == id).ToListAsync();
                return roasters.Count() > 0 ? roasters.First() : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task Update(Roaster entity)
        {
            try
            {
                Context.Roasters.Update(entity);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Roaster repository update method failed to complete");
            }

        }

        public async Task<List<Roaster>> GetList()
        {
            try
            {
                return await Context.Roasters.ToListAsync();
            }
            catch { return null; }
        }

        public async Task<Roaster> GetSingleByAddressId(Guid addressId)
        {
            try
            {
                var roaster = await Context.Roasters.Where(node => node.OfficeAddressId == addressId).ToListAsync();
                return roaster.Count() > 0 ? roaster.First() : null;
            }
            catch { return null; }
        }

        public async Task<Roaster> GetRoaster(Roaster entity)
        {
            try
            {
                var roasters = await Context.Roasters.Where(node => (node.Name == entity.Name && node.OfficeAddressId == entity.OfficeAddressId
                  && node.ContactEmail == entity.ContactEmail && node.ContactNumber == entity.ContactNumber && node.WebSiteLink == entity.WebSiteLink
                  && node.InstagramProfileLink == entity.InstagramProfileLink && node.VkProfileLink == entity.VkProfileLink
                  && node.TelegramProfileLink == entity.TelegramProfileLink
                  )).ToListAsync();
                return roasters.Count() > 0 ? roasters.First() : null;
            }
            catch { return null; }
        }

        public async Task<Roaster> GetRoasterByName(string roasterName)
        {
            try
            {
                var roasters = await Context.Roasters.Where(node => node.Name == roasterName).ToListAsync();
                return roasters.Count() > 0 ? roasters.First() : null;
            }
            catch { return null; }
        }
    }
}
