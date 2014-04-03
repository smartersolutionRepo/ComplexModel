namespace ComplexModelData
{
using ComplexModelCore.Model;

    using ComplexModelCore.Interfaces.Repositories;

 
    public partial class CustomerRepository : BaseRepository<Customers>, ICustomerRepository
    {
        public CustomerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}