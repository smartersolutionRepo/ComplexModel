namespace ComplexModelCore.Interfaces.Repositories

{
    public interface IUnitOfWork
    {
        int Commit();
    }
}