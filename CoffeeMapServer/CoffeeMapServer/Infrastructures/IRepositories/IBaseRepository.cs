using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IBaseRepository<T>
    {
        public Task Create(T entity);
        
        public Task<T> GetSingle(Guid id);
        
        public Task Update(T entity);
        
        public Task Delete(Guid id);
        
        public Task<List<T>> GetList();
    }
}
