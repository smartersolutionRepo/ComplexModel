namespace ComplexModelCore.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

     

    /// <summary>
    /// The generic base interface for all repositories...
    /// Purpose:
    /// - Implement this on the repository... Regardless of datasource... Xml, MSSQL, MYSQL etc..
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllReadOnly();

        T GetById(object id);

        void SaveOrUpdate(T entity);
        void Update(T entity);

        void Delete(T entity);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100);

     
    }
}