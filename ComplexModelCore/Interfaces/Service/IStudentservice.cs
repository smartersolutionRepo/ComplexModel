namespace ComplexModelCore.Interfaces.Service
{
    using ComplexModelCore.Model;
   

    public partial interface IStudentService : IService<student>
    {
        bool AddStudent (student stu);
        bool UpdateStudent(student stu);
	}
}