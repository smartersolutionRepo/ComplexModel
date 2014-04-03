namespace ComplexModelData
{
    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
 
  
 

    /// <summary>
    /// An abstract baseclass handling basic CRUD operations against the context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IDisposable, IRepository<T> where T : DomainObject
    {
        protected readonly IDbSet<T> _dbset;
        protected readonly IDatabaseFactory _databaseFactory;
        private IDataContext _context;

        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
            this._dbset = this.DataContext.DbSet<T>();
        }

        public virtual IQueryable<T> Query
        {
            get { return _dbset; }
        }

        public IDataContext DataContext
        {
            get { return this._context ?? (this._context = this._databaseFactory.Get()); }
        }

        protected string EntitySetName { get; set; }

        public virtual void SaveOrUpdate(T entity)
        {
            if (UnitOfWork.IsPersistent(entity))
            {
                this.DataContext.Entry(entity).State = EntityState.Modified;

            }
            else
                this._dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            this.DataContext.Entry(entity).State = EntityState.Modified;

        }

        public virtual T GetById(object id)
        {
            foreach (var item in this.Query)
            {
                //if (item.Id.Equals(id))
                //    return item;
            }
            return null;
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.Query;
        }

        public virtual IQueryable<T> GetAllReadOnly()
        {
            return this.Query.AsNoTracking();
        }

        public virtual void Delete(T entity)
        {
            this._dbset.Remove(entity);
        }

        public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> expression, int maxHits = 100)
        {
            return this.Query.Where(expression).Take(maxHits);
        }

     

        public void Dispose()
        {
            this.DataContext.ObjectContext().Dispose();
        }
    }
}