using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire);

        Task<IDictionary<string, T>> Lookup<T>(string key);

        Task<bool> Contains(string key);

        Task Set<T>(string key, T data, int cacheTime);

        Task Remove(string key);

        Task Update<T>(string key, T data);

        Task Clear();
    }
}