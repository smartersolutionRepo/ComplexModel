namespace ComplexModelData
{
using ComplexModelCore.Model;

    using ComplexModelCore.Interfaces.Repositories;

 
    public partial class StudentRepository : BaseRepository<student>, IStudentRepository
    {
        public StudentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}