using Adopcat.Data.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

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

        public virtual TObject Find(System.Linq.Expressions.Expression<Func<TObject, bool>> filterExpression)
        {
            try
            {
                return DbSet.AsNoTracking().FirstOrDefault(filterExpression);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IQueryable<TObject> Create(IQueryable<TObject> TObjects)
        {
            try
            {
                foreach (var TObject in TObjects)
                    this.Create(TObject);

                return TObjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TObject Create(TObject TObject)
        {
            try
            {
                var newEntry = DbSet.Add(TObject);

                if (!this.ShareContext)
                {
                    Context.SaveChanges();
                    Context.Entry(newEntry).State = EntityState.Detached;
                }
                return newEntry;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Delete(TObject TObject)
        {
            try
            {
                DbSet.Attach(TObject);
                DbSet.Remove(TObject);

                if (!this.ShareContext)
                    return Context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Delete(Expression<Func<TObject, bool>> filterExpression)
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
                    return Context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Update(TObject TObject)
        {
            try
            {
                var entry = Context.Entry(TObject);
                DbSet.Attach(TObject);
                entry.State = EntityState.Modified;

                if (!this.ShareContext)
                    return Context.SaveChanges();
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

        # endregion
    }
}
