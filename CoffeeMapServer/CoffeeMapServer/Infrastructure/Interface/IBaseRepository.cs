using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IBaseRepository<T>
    {
        public void Add(T entity);

        public Task<T> GetSingleAsync(Guid id,
                                      [CallerMemberName] string methodName = "");

        public void Update(T entity);

        public void Delete(T id);

        public Task<IList<T>> GetListAsync([CallerMemberName] string methodName = "");

        public Task SaveChangesAsync();
    }
}