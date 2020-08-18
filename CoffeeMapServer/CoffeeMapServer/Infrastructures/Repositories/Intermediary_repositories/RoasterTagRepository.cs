using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories.Intermediary_repositories
{
    public class RoasterTagRepository : IRoasterTagRepository
    {
        private readonly CoffeeDbContext context;
        public RoasterTagRepository(CoffeeDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task Create(RoasterTag entity)
        {
            await context.RoasterTags.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter noteId = new SqlParameter("@noteId", id);
            await context.Database.ExecuteSqlRawAsync("DELETE FROM RoasterTags WHERE Id=@noteId", noteId);
            await context.SaveChangesAsync();
        }
        public async Task Delete(int roasterId, int TagId)
        {
            SqlParameter _roasterId = new SqlParameter("@roasterId", roasterId);
            SqlParameter _tagId = new SqlParameter("@tagId", TagId);
            await context.Database.ExecuteSqlRawAsync("DELETE FROM RoasterTags WHERE RoasterId=@roasterId AND TagId=@tagId", _roasterId, _tagId);
            await context.SaveChangesAsync();
        }
        public async Task<List<RoasterTag>> GetList()
        {
            return await context.RoasterTags.ToListAsync();
        }

        public async Task<RoasterTag> GetSingle(int id)
        {
            SqlParameter noteId = new SqlParameter("@noteId", id);
            var RoasterTag = await context.RoasterTags.FromSqlRaw("SELECT * FROM RoasterTags WHERE Id=@noteId", noteId).ToListAsync();
            return RoasterTag.Count() >= 1 ? RoasterTag.First() : null;
        }

        public async Task Update(RoasterTag entity)
        {
            SqlParameter noteId = new SqlParameter("@noteId", entity.Id);
            SqlParameter roasterId = new SqlParameter("@roasterId", entity.RoasterId);
            SqlParameter tagId = new SqlParameter("@tagId", entity.TagId);
            await context.Database.ExecuteSqlRawAsync("UPDATE RoasterTags SET RoasterId=@roasterId, TagId=@tagId WHERE Id=@noteId",noteId, roasterId, tagId);
            await context.SaveChangesAsync();
        }
        public async Task<List<RoasterTag>> GetPairsByRoasterId(int roasterId)
        {
            SqlParameter _roasterId = new SqlParameter("@roasterId", roasterId);
           return await context.RoasterTags.FromSqlRaw("SELECT * FROM RoasterTags WHERE RoasterId=@roasterId", _roasterId).ToListAsync();
            
        }
        public async Task<List<RoasterTag>> GetPairsByTagId(int id)
        {
            SqlParameter _tagId = new SqlParameter("@tagId", id);
            return await context.RoasterTags.FromSqlRaw("SELECT * FROM RoasterTags WHERE TagId=@tagId", _tagId).ToListAsync();
        }

    }
}
