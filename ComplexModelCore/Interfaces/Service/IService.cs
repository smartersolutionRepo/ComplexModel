
    namespace ComplexModelCore.Interfaces.Service
{

    using ComplexModelCore.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.ServiceModel;


        [ServiceContract]
        public interface IService<T>
        {
            [OperationContract]
            IQueryable<T> GetAll();

            [OperationContract]
            IQueryable<T> GetAllReadOnly();

            [OperationContract]
            T GetById(object id);

            [OperationContract]
            IValidationContainer<T> SaveOrUpdate(T entity);
            [OperationContract]
            IValidationContainer<T> Update(T entity);

            [OperationContract]
            void Delete(T entity);

            [OperationContract]
            IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100);

           
        }
}
