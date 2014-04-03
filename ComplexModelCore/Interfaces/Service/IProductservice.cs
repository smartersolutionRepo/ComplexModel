namespace ComplexModelCore.Interfaces.Service
{
    using ComplexModelCore.Model;
   

    public partial interface IProductService : IService<Product>
    {
        bool AddProduct (Product pro);
        bool UpdateProduct(Product pro);
	}
}