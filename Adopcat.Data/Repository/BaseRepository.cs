using Adopcat.Data.Interfaces;
using Adopcat.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Adopcat.Data.Repository
{
    /// <summary>
    /// Classe genérica para manipulação dos dados no banco.
    /// </summary>
    /// <typeparam name="TObject">Entidade sendo manipulada.</typeparam>
    public abstract class BaseRepository<TObject> : IBaseRepository<TObject> where TObject : class
    {
        # region Fields

        protected DatabaseContext Context;
        private bool ShareContext;

        # endregion

        # region Constructor

        public BaseRepository(IDbContextFactory context)
        {
            this.Context = context.GetDbContext();
        }

        protected BaseRepository(DatabaseContext context)
        {
            this.Context = context;
            this.ShareContext = true;
        }

        # endregion

        # region Properties

        protected DbSet<TObject> DbSet
        {
            get
            {
                return Context.Set<TObject>();
            }
        }

        # endregion

        # region Inherited Methods

        public void Dispose()
        {
            if (!this.ShareContext && (Context != null))
                Context.Dispose();
        }

        # endregion

        # region Methods

        public virtual IQueryable<TObject> GetAll()
        {
            try
            {
                return DbSet.AsNoTracking().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IQueryable<TObject> GetAll(Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                return DbSet.Where(filterExpression).AsNoTracking().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IQueryable<TObject> GetAll(Expression<Func<TObject, bool>> filterExpression, out int total, [System.Runtime.InteropServices.OptionalAttribute][System.Runtime.InteropServices.DefaultParameterValueAttribute(0)]int index, [System.Runtime.InteropServices.OptionalAttribute][System.Runtime.InteropServices.DefaultParameterValueAttribute(50)]int size)
        {
            try
            {
                int skipCount = index * size;
                var resetSet = filterExpression != null ? DbSet.Where(filterExpression).AsNoTracking().AsQueryable() : DbSet.AsNoTracking().AsQueryable();

                total = resetSet.Count();

                resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);

                return resetSet.AsNoTracking().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TObject>> GetAllAsync()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TObject>> GetAllAsync(Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                var query = DbSet.Where(filterExpression).AsQueryable();
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<TObject>> GetAllAsync(Expression<Func<TObject, bool>> filterExpression, out int total, int index = 0, int size = 50)
        {
            try
            {
                int skipCount = index * size;
                var resetSet = filterExpression != null ? DbSet.Where(filterExpression).AsNoTracking().AsQueryable() : DbSet.AsNoTracking().AsQueryable();

                total = resetSet.Count();

                resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);

                return resetSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Contains(Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                return DbSet.Count(filterExpression) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<TObject> FindAsync(Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                return await DbSet.FindAsync(filterExpression);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<IQueryable<TObject>> CreateAsync(IQueryable<TObject> TObjects)
        {
            try
            {
                foreach (var TObject in TObjects)
                    await CreateAsync(TObject);

                return TObjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async virtual Task<TObject> CreateAsync(TObject TObject)
        {
            try
            {
                var newEntry = DbSet.Add(TObject);

                if (!this.ShareContext)
                {
                    await Context.SaveChangesAsync();
                    Context.Entry(newEntry).State = EntityState.Detached;
                }
                return newEntry;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<int> DeleteAsync(TObject TObject)
        {
            try
            {
                DbSet.Attach(TObject);
                DbSet.Remove(TObject);

                if (!this.ShareContext)
                    return await Context.SaveChangesAsync();
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<int> DeleteAsync(Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                var objects = GetAll(filterExpression);

                foreach (var obj in objects)
                {
                    DbSet.Attach(obj);
                    DbSet.Remove(obj);
                }

                if (!this.ShareContext)
                    return await Context.SaveChangesAsync();
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<int> UpdateAsync(TObject TObject)
        {
            try
            {
                var entry = Context.Entry(TObject);
                DbSet.Attach(TObject);
                entry.State = EntityState.Modified;

                if (!this.ShareContext)
                    return await Context.SaveChangesAsync();
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Count()
        {
            try
            {
                return DbSet.Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Count(System.Linq.Expressions.Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                return DbSet.Count(filterExpression);
            }
            catch (Exception)
            {
                throw;
            }
        }       

        #endregion
    }
}
