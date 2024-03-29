﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class RoasterRequestRepository : IRoasterRequestRepository
    {
        private readonly CoffeeDbContext _context;

        public RoasterRequestRepository(CoffeeDbContext context,
                                        ILogger logger)
            => _context = context ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(RoasterRequest entity)
            => _context.RoasterRequests.Add(entity);

        public void Delete(RoasterRequest entity)
            => _context.RoasterRequests.Remove(entity);

        public void DeleteRoasterRequest(IList<RoasterRequest> range)
            => _context.RoasterRequests.RemoveRange(range);

        public async Task<RoasterRequest> GetSingleAsync(Guid id,
                                                         [CallerMemberName] string methodName = "")
            => await _context.RoasterRequests
            .TagWith($"{nameof(RoasterRequestRepository)}.{methodName} ({id})")
            .Include(rr=>rr.Picture)
            .FirstOrDefaultAsync(node => node.Id == id);

        public void Update(RoasterRequest entity)
            => _context.RoasterRequests.Update(entity);

        public async Task<IList<RoasterRequest>> GetListAsync([CallerMemberName] string methodName = "")
            => await _context.RoasterRequests
               .OrderBy(r => r.Roaster.CreationDate)
               .TagWith($"{nameof(RoasterRequestRepository)}.{methodName}")
               .ToListAsync();

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}