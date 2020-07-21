using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IBaseRepository<T>
    {
        public Task Create(T entity);
        public Task<T> GetSingle(int id);
        public Task Update(T entity);
        public Task Delete(int id);
        public Task<List<T>> GetList();
    }
}
