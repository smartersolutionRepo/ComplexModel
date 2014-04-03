namespace ComplexModelCore.Interfaces.Repositories
{
    using ComplexModelCore.Model;
    using System;

    public interface IDatabaseFactory : IDisposable
    {
        IDataContext Get();
    }
}