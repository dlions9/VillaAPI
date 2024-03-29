﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaAPI.Data;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();

        }
        
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();

        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
            
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
