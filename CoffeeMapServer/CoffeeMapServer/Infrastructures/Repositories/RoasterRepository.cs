﻿using CoffeeMapServer.EF;
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
    public class RoasterRepository: IRoasterRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public RoasterRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(Roaster entity)
        {
            await DbContext.Roasters.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Roasters WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Roaster> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.Roasters.FromSqlRaw("SELECT * FROM Roasters WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Roaster entity)
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
        public async Task<List<Roaster>> GetList()
        {
            return await DbContext.Roasters.ToListAsync();
        }
    }
}
