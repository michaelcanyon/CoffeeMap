using CoffeeMapServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface ITagRepository: IBaseRepository<Tag>
    {
        public Task<Tag> GetSingle(string title);
    }
}
