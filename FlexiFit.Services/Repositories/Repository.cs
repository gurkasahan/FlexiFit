using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FlexiFit.Entities;

namespace FlexiFit.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FlexiFitDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(FlexiFitDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }

}
