using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class RoasterRequestRepository : IRoasterRequestRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public RoasterRequestRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }

        public async Task Create(RoasterRequest entity)
        {
            try
            {
                await DbContext.RoasterRequests.AddAsync(entity);
                await DbContext.SaveChangesAsync();
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
                SqlParameter paramid = new SqlParameter("@id", id);
                await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM RoasterRequests WHERE Id=@id", paramid);
                await DbContext.SaveChangesAsync();
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
                await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM RoasterRequests");
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
                SqlParameter paramid = new SqlParameter("@id", id);
                var list = await DbContext.RoasterRequests.FromSqlRaw("SELECT * FROM RoasterRequests WHERE Id=@id", paramid).ToListAsync();
                return list.Count() > 0 ? list.First() : null;
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
                SqlParameter paramid = new SqlParameter("@id", entity.Id);
                SqlParameter name = new SqlParameter("@name", entity.Name);
                SqlParameter email = new SqlParameter("@email", entity.ContactEmail);
                SqlParameter tags = new SqlParameter("@tags", entity.TagString);
                SqlParameter phone = new SqlParameter("@phone", entity.ContactNumber);
                SqlParameter website = new SqlParameter("@website", entity.WebSiteLink);
                SqlParameter instagram = new SqlParameter("@instagram", entity.InstagramProfileLink);
                SqlParameter vk = new SqlParameter("@vk", entity.VkProfileLink);
                SqlParameter telegram = new SqlParameter("@telegram", entity.TelegramProfileLink);
                SqlParameter address = new SqlParameter("@address", entity.AddressStr);
                SqlParameter openingHours = new SqlParameter("@openingHours", entity.OpeningHours);

                await DbContext.Database.ExecuteSqlRawAsync("UPDATE RoasterRequests SET Name=@name, ContactEmail=@email," +
                    " ContactNumber=@phone, WebSiteLink=@website, InstagramProfileLink=@instagram, VkProfileLink=@vk," +
                    " TelegramProfileLink=@telegram, AddressStr=@address, OpeningHours=@openingHours, TagString=@tags WHERE Id=@id"
                    , paramid, name, email, phone, website, instagram, vk, telegram, address, openingHours, tags);
                await DbContext.SaveChangesAsync();
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
                return await DbContext.RoasterRequests.ToListAsync();
            }
            catch { return null; }
        }
    }
}
