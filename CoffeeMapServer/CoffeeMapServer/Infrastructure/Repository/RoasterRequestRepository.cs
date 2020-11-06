using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class RoasterRequestRepository : IRoasterRequestRepository
    {
        CoffeeDbContext Context { get; set; }

        public RoasterRequestRepository(CoffeeDbContext context)
            => Context = context;

        public void Add(RoasterRequest entity)
            => Context.RoasterRequests.Add(entity);

        public void Delete(RoasterRequest entity)
            => Context.RoasterRequests.Remove(entity);

        public void DeleteRange(IList<RoasterRequest> range)
            => Context.RoasterRequests.RemoveRange(range);

        public async Task<RoasterRequest> GetSingleAsync(Guid id)
            => await Context.RoasterRequests.FirstOrDefaultAsync(node => node.Id == id);
        //TODO: don't forget to fix nullable fields in argument entitites
        public void Update(RoasterRequest entity)
            => Context.RoasterRequests.Update(entity);

        public async Task<IList<RoasterRequest>> GetListAsync()
            => await Context.RoasterRequests.ToListAsync();

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();
    }
}
