namespace ComplexModelCore.Interfaces.Service
{
    using ComplexModelCore.Model;
   

    public partial interface ICustomerService : IService<Customers>
    {
        bool AddCustomer (Customers cus);
        bool UpdateCustomer(Customers cus);
	}
}