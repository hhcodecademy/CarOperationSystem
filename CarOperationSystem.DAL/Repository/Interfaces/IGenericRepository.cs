using CarOperationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T> where T :BaseEntity ,new()
    {
        public Task<T> Add(T entity);
        public T Update(T entity);
        public Task<T> Get(int id);
        public Task<IQueryable<T>> GetAll();
        public T Remove(int id);
    }
}
