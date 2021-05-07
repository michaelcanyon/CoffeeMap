using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructure
{
    public static class RepositoryExtensions
    {
        public static IQueryable<T> CustomTagWith<T>(this IQueryable<T> source,
                                                     [CallerMemberName] string methodName = "")
            => source.TagWith(methodName);
    }
}
