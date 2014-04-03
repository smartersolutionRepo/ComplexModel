namespace ComplexModelData
{
using ComplexModelCore.Model;
    using ComplexModelCore.Interfaces.Repositories;
  

 
    public partial class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}