using CoffeeMapServer.Models;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        public Task<Tag> GetSingleAsync(string title);
    }
}
