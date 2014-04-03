namespace ComplexModelData
{
    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Model;





    public partial class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}