namespace ComplexModelCore.Interfaces.Service
{
    using ComplexModelCore.Model;
   

    public partial interface IPersonService : IService<Person>
    {
        bool AddPerson (Person per);
        bool UpdatePerson (Person per);
	}
}