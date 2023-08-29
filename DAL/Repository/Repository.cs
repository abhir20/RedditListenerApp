using DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CustomDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public Repository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
    }
}
