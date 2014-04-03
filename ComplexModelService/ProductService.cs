
namespace ComplexModelService
{
    #region

    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Interfaces.Service;
    using ComplexModelCore.Model;
    using ComplexModelService;

    #endregion

    public partial class ProductService : BaseService<Product>, IProductService
    {
        protected new IProductRepository Repository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository ProductRepository)
            : base(unitOfWork)
        {
            base.Repository = Repository = ProductRepository;
        }

        public virtual bool AddProduct(Product pro)
        {
            try
            {

                this.SaveOrUpdate(pro);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool UpdateProduct(Product pro)
        {
            try
            {
                this.SaveOrUpdate(pro);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}