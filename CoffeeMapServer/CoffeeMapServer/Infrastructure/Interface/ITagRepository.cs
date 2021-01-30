using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        public Task<Tag> GetSingleAsync(string title,
                                        [CallerMemberName] string methodName = "");

        public Task<Tag> GetSingleAsNoTrackingAsync(string title,
                                                    [CallerMemberName] string methodName = "");

        public Task<IList<Tag>> GetTagsByTagIds(IList tagsIds,
                                                [CallerMemberName] string methodName = "");
    }
}