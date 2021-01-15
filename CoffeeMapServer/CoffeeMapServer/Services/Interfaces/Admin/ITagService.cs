using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface ITagService
    {
        public Task<IList<Tag>> FetchTagsListAsync();

        public Task<Tag> FetchSingleTagAsync(Guid id);

        public Task<int> UpdateSingleTagAsync(Tag tag);

        public Task<int> DeleteTagAsync(Guid id);
    }
}