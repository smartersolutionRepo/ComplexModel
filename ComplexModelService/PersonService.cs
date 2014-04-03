
namespace ComplexModelService
{
    #region

    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Interfaces.Service;
    using ComplexModelCore.Model;
    using ComplexModelService;

    #endregion

    public partial class PersonService : BaseService<Person>, IPersonService
    {
        protected new IPersonRepository Repository;

        public PersonService(IUnitOfWork unitOfWork, IPersonRepository PersonRepository)
            : base(unitOfWork)
        {
            base.Repository = Repository = PersonRepository;
        }

        public virtual bool AddPerson(Person Per)
        {
            try
            {

                this.SaveOrUpdate(Per);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool UpdatePerson(Person Per)
        {
            try
            {
                this.SaveOrUpdate(Per);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}