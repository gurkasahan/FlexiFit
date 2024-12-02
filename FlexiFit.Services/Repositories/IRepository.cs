using System.Collections.Generic;

namespace FlexiFit.Services.Repositories
{
    /// <summary>
    /// Principal Author: [Your Name]
    /// Interface defining basic CRUD operations for a repository.
    /// </summary>
    /// <typeparam name="T">The entity type that the repository manages.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity from the repository by its ID.
        /// </summary>
        void Delete(int id);
    }
}
