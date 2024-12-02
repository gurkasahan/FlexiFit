using FlexiFit.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FlexiFit.Services.Repositories
{
    /// <summary>
    /// Principal Author: [Your Name]
    /// A generic repository class that provides basic CRUD operations for entities in the database.
    /// </summary>
    /// <typeparam name="T">The entity type that this repository manages.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FlexiFitDBContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes the repository with the provided DbContext.
        /// </summary>
        /// <param name="context">The database context for accessing the database.</param>
        public Repository(FlexiFitDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed
                throw new Exception("An error occurred while adding the entity to the database.", ex);
            }
        }

        /// <summary>
        /// Deletes an entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
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

        /// <summary>
        /// Retrieves all entities from the database.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
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

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
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

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
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
