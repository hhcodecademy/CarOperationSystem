using CarOperationSystem.DAL.Data;
using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly CustomDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }


        public async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return _dbSet;
        }

        public T Remove(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
            return entity;


        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
