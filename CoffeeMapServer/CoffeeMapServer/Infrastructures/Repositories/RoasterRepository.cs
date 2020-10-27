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
    public class RoasterRepository : IRoasterRepository
    {
        CoffeeDbContext DbContext { get; set; }
        
        public RoasterRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        
        public async Task Create(Roaster entity)
        {
            try
            {
                await DbContext.Roasters.AddAsync(entity);
                await DbContext.SaveChangesAsync();
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
                SqlParameter paramid = new SqlParameter("@id", id);
                await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Roasters WHERE Id=@id", paramid);
                await DbContext.SaveChangesAsync();
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
                SqlParameter paramid = new SqlParameter("@id", id);
                var list = await DbContext.Roasters.FromSqlRaw("SELECT * FROM Roasters WHERE Id=@id", paramid).ToListAsync();
                return list.Count() > 0 ? list.First() : null;
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
                SqlParameter paramid = new SqlParameter("@id", entity.Id);
                SqlParameter name = new SqlParameter("@name", entity.Name);
                SqlParameter officeId = new SqlParameter("@officeId", entity.OfficeAddressId);
                SqlParameter email = new SqlParameter("@email", entity.ContactEmail);
                SqlParameter phone = new SqlParameter("@phone", entity.ContactNumber);
                SqlParameter website = new SqlParameter("@website", entity.WebSiteLink);
                SqlParameter instagram = new SqlParameter("@instagram", entity.InstagramProfileLink);
                SqlParameter vk = new SqlParameter("@vk", entity.VkProfileLink);
                SqlParameter telegram = new SqlParameter("@telegram", entity.TelegramProfileLink);

                await DbContext.Database.ExecuteSqlRawAsync("UPDATE Roasters SET Name=@name, OfficeAddressId=@officeId, ContactEmail=@email" +
                    ", ContactNumber=@phone, WebSiteLink=@website, InstagramProfileLink=@instagram, VkProfileLink=@vk, TelegramProfileLink=@telegram WHERE Id=@id"
                    , paramid, name, officeId, email, phone, website, instagram, vk, telegram);
                await DbContext.SaveChangesAsync();
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
                return await DbContext.Roasters.ToListAsync();
            }
            catch { return null; }
        }
        public async Task<Roaster> GetSingleByAddressId(Guid addressId)
        {
            try
            {
                SqlParameter paramid = new SqlParameter("@id", addressId);
                var list = await DbContext.Roasters.FromSqlRaw("SELECT * FROM Roasters WHERE OfficeAddressId=@id", paramid).ToListAsync();
                return list.Count() > 0 ? list.First() : null;
            }
            catch { return null; }
        }

        public async Task<Roaster> GetRoaster(Roaster entity)
        {
            try
            {
                SqlParameter name = new SqlParameter("@name", entity.Name);
                SqlParameter officeId = new SqlParameter("@officeId", entity.OfficeAddressId);
                SqlParameter email = new SqlParameter("@email", entity.ContactEmail);
                SqlParameter phone = new SqlParameter("@phone", entity.ContactNumber);
                SqlParameter website = new SqlParameter("@website", entity.WebSiteLink);
                SqlParameter instagram = new SqlParameter("@instagram", entity.InstagramProfileLink);
                SqlParameter vk = new SqlParameter("@vk", entity.VkProfileLink);
                SqlParameter telegram = new SqlParameter("@telegram", entity.TelegramProfileLink);
                var _roasters = await DbContext.Roasters.FromSqlRaw("SELECT * FROM Roasters WHERE Name=@name AND OfficeAddressId=@officeId AND ContactEmail=@email" +
                     " AND ContactNumber=@phone AND WebSiteLink=@website AND InstagramProfileLink=@instagram AND VkProfileLink=@vk AND TelegramProfileLink=@telegram"
                     , name, officeId, email, phone, website, instagram, vk, telegram).ToListAsync();
                return _roasters.Count() > 0 ? _roasters.First() : null;
            }
            catch { return null; }
        }
        public async Task<Roaster> GetRoasterByName(string roasterName)
        {
            try
            {
                SqlParameter name = new SqlParameter("@name", roasterName);
                var _roasters = await DbContext.Roasters.FromSqlRaw("SELECT * FROM Roasters WHERE Name=@name"
                     , name).ToListAsync();
                return _roasters.Count() > 0 ? _roasters.First() : null;
            }
            catch { return null; }
        }
    }
}
