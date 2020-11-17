using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        public Task<Tag> GetSingleAsync(string title);

        public Task<IList<Tag>> GetTagsByTagIds(IList tagsIds);
    }
}