﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IRoasterAdminService
    {
        public Task<IList<Roaster>> FetchRoastersAsync();

        public Task<Roaster> FetchSingleRoasterAsync(Guid id);

        public Task<IList<RoasterTag>> FetchRoasterTagsAsync(Guid roasterId);

        public Task<IList<RoasterTag>> FetchRoasterTagsAsync();

        public Task<Tag> FetchTagByIdAsync(Guid id);

        public Task<IList<Tag>> FetchTagsAsync();

        public Task<int> UpdateRoasterAsync(Roaster entity, string newTags, string deletableTags, IFormFile picture);

        public Task<int> AddRoasterAsync(Roaster roaster, string tags, Address address, IFormFile picture);

        public Task<int> DeleteRoasterByIdAsync(Guid id);
    }
}