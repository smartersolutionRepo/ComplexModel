namespace ComplexModelData
{
using ComplexModelCore.Model;

    using ComplexModelCore.Interfaces.Repositories;
  

 
    public partial class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}