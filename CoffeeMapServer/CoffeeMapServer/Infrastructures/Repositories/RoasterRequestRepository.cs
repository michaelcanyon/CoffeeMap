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
    public class RoasterRequestRepository : IRoasterRequestRepository
    {
        CoffeeDbContext Context { get; set; }

        public RoasterRequestRepository(CoffeeDbContext context)
        {
            Context = context;
        }

        public async Task Create(RoasterRequest entity)
        {
            try
            {
                await Context.RoasterRequests.AddAsync(entity);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("RoasterRequest repository create method failed to complete");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var roasterRequest = (await Context.RoasterRequests.Where(node => node.Id == id).ToListAsync()).First();
                Context.RoasterRequests.Remove(roasterRequest);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("RoasterRequest repository delete method failed to complete");
            }
        }

        public async Task DeleteAll()
        {
            try
            {
                var roasterRequests = await Context.RoasterRequests.ToListAsync();
                Context.RoasterRequests.RemoveRange(roasterRequests);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("RoasterRequest repository DeleteAll method failed to complete");
            }
        }

        public async Task<RoasterRequest> GetSingle(Guid id)
        {
            try
            {
                var roasterRequest = await Context.RoasterRequests.Where(node => node.Id == id).ToListAsync();
                return roasterRequest.Count() > 0 ? roasterRequest.First() : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task Update(RoasterRequest entity)
        {
            if (string.IsNullOrEmpty(entity.WebSiteLink))
                entity.WebSiteLink = "none";
            if (string.IsNullOrEmpty(entity.VkProfileLink))
                entity.VkProfileLink = "none";
            if (string.IsNullOrEmpty(entity.InstagramProfileLink))
                entity.InstagramProfileLink = "none";
            if (string.IsNullOrEmpty(entity.TelegramProfileLink))
                entity.TelegramProfileLink = "none";
            try
            {
                Context.RoasterRequests.Update(entity);
                await Context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("RoasterRequest repository update method failed to complete");
            }

        }

        public async Task<List<RoasterRequest>> GetList()
        {
            try
            {
                return await Context.RoasterRequests.ToListAsync();
            }
            catch { return null; }
        }
    }
}
