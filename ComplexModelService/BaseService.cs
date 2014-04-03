namespace ComplexModelService
{
    using System;
    using System.Collections.Generic;
    
    using System.Data.Entity;
    using System.Linq;
    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Interfaces.Service;
    using ComplexModelCore.Model;
    using ComplexModelCore.Validation;
    using ComplexModelService.Common.Validation;
    using System.Data;
    using System.Data.Objects;
    
     

    /// <summary>
    /// Base for all services... If you need specific businesslogic
    /// override these methods in inherited classes and implement the logic there.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IService<T> where T : DomainObject
    {
        protected IRepository<T> Repository;

        protected IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual IQueryable<T> GetAllReadOnly()
        {
            return Repository.GetAllReadOnly();
        }

        public virtual T GetById(object id)
        {
            return Repository.GetById(id);
        }

        public virtual IValidationContainer<T> Update(T entity)
        {


            var validation = entity.GetValidationContainer();
            try
            {

                if (!validation.IsValid)
                    return validation;

                //Repository.SaveOrUpdate(entity);
                Repository.Update(entity);
                UnitOfWork.Commit();
                return validation;
            }
            catch (OptimisticConcurrencyException ex)
            {
                ObjectStateEntry entry = ex.StateEntries[0];
            
              
                return validation;
            }


        }
     

        public virtual IValidationContainer<T> SaveOrUpdate(T entity)
        {
            var validation = entity.GetValidationContainer();
            try
            {

                if (!validation.IsValid)
                    return validation;

                //Repository.SaveOrUpdate(entity);
                Repository.SaveOrUpdate(entity);
                UnitOfWork.Commit();
                return validation;
            }
            catch (OptimisticConcurrencyException ex)
            {
                ObjectStateEntry entry = ex.StateEntries[0];
            
            
                return validation;
            }


        }

        public virtual void Delete(T entity)
        {
            Repository.Delete(entity);
            UnitOfWork.Commit();
        }

        public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> expression, int maxHits = 100)
        {
            return Repository.Find(expression, maxHits);
        }

      
    }
}
