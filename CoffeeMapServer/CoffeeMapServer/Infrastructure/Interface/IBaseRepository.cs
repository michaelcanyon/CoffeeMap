using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IBaseRepository<T>
    {
        public void Add(T entity);
        
        public Task<T> GetSingleAsync(Guid id);
        
        public void Update(T entity);
        
        public void Delete(T id);
        
        public Task<IList<T>> GetListAsync();

        public Task SaveChangesAsync();
    }
}