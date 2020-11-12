using ContactManager.Core.Repositories;
using ContactManager.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data.Sql.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public ContactManagerDataContext _context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(ContactManagerDataContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Attach(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
