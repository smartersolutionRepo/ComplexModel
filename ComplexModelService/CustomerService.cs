
namespace ComplexModelService
{
    #region

    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Interfaces.Service;
    using ComplexModelCore.Model;
    using ComplexModelService;

    #endregion

    public partial class CustomerService : BaseService<Customers>, ICustomerService
    {
        protected new ICustomerRepository Repository;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository CustomerRepository)
            : base(unitOfWork)
        {
            base.Repository = Repository = CustomerRepository;
        }

        public virtual bool AddCustomer(Customers cus)
        {
            try
            {

                this.SaveOrUpdate(cus);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool UpdateCustomer(Customers cus)
        {
            try
            {
                this.SaveOrUpdate(cus);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}