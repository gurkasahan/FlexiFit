// FlexiFit.Services/Repositories/Repository.cs
using FlexiFit.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FlexiFit.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FlexiFitDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(FlexiFitDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                // For example, you can log the exception or rethrow it
                throw new Exception("An error occurred while adding the entity to the database.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity from the database.", ex);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving entities from the database.", ex);
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity from the database.", ex);
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity in the database.", ex);
            }
        }
    }
}
