namespace ComplexModelCore.Interfaces.Repositories
{
    using ComplexModelCore.Model;
    using System.Data.Entity;
    using System.Data.Objects;
    using System.Data.Entity.Infrastructure;
    
    

   

    public interface IDataContext
    {
        ObjectContext ObjectContext();
        IDbSet<T> DbSet<T>() where T : DomainObject;
        DbEntityEntry Entry<T>(T entity) where T : DomainObject;
        void Dispose();
    }
}