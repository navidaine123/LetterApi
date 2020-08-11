using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();

        Task<T> GetAsync(object key);

        Task<T> AddAsync(T t);

        Task<T> UpdateAsync(T t, object key);

        T Delete(T t);

        
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SmContext _smContext;
        private DbSet<T> _entities;

        public GenericRepository(SmContext smContext)
        {
            _smContext = smContext;
            _entities = _smContext.Set<T>();
        }

        public async Task<T> AddAsync(T t)
        {
            await _entities.AddAsync(t);
            return t;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetAsync(object key)
        {
            return await _entities.FindAsync(key);
        }

        public async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            var exist = await _entities.FindAsync(key);

            if (exist != null)
            {
                _smContext.Entry(exist).CurrentValues.SetValues(t);
            }

            return exist;
        }

        public T Delete(T t)
        {
            _entities.Remove(t);
            return t;
        }

        

    }
}