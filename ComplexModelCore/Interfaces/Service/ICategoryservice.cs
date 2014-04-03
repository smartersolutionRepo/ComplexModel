namespace ComplexModelCore.Interfaces.Service
{
    using ComplexModelCore.Model;
    public partial interface ICategoryService : IService<Category>
    {
        bool AddCategory (Category cat);
        bool UpdateCategory (Category cat);
	}
}