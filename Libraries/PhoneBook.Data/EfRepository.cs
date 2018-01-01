#region Usings

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using PhoneBook.Core.Data;

#endregion

namespace PhoneBook.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        #endregion

        #region Ctors

        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Utils

        private void RollbackEntityChanges(DbEntityValidationException dbEx)
        {
            foreach (var entry in dbEx.EntityValidationErrors.Select(error => error.Entry))
            {
                if (entry == null)
                    continue;

                entry.State = EntityState.Unchanged;
            }
            _context.SaveChanges();
        }

        #endregion

        #region Methods

        public T GetById(object id) => Entities.Find(id);

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                RollbackEntityChanges(dbEx);
                throw;
            }
        }

        public void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                RollbackEntityChanges(dbEx);
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                RollbackEntityChanges(dbEx);
                throw;
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                RollbackEntityChanges(dbEx);
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                RollbackEntityChanges(dbEx);
                throw;
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                RollbackEntityChanges(dbEx);
                throw;
            }
        }

        #endregion

        #region Props

        private IDbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        #endregion
    }
}