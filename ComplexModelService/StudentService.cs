
namespace ComplexModelService
{
    #region

    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelCore.Interfaces.Service;
    using ComplexModelCore.Model;
    using ComplexModelData;
    using ComplexModelService;
    using System.Collections.Generic;
    using System.Data;

    #endregion

    public partial class StudentService : BaseService<student>, IStudentService
    {
        protected new IStudentRepository Repository;

        public StudentService(IUnitOfWork unitOfWork, IStudentRepository StudentRepository)
            : base(unitOfWork)
        {
            base.Repository = Repository = StudentRepository;
        }

        public virtual bool AddStudent(student stu)
        {
            try
            {
                using (var context = new DataContext())
                {

                    var student = new student();
                    student.studentname = stu.studentname;
                    student.studentclass = stu.studentclass;
                    student.Age = stu.Age;
                    student.customerID = stu.customerID;
                    student.products = new List<Product>();
                    context.Students.Add(student);
                    context.SaveChanges();
                    foreach (var item in stu.products)
                    {
                        var prod = new Product();
                        prod.Id = item.Id;
                        prod.Quantity = item.Quantity;
                        prod.ProductName = item.ProductName;
                        prod.ProductDesc = item.ProductDesc;
                        student.products.Add(prod);
                        context.Entry(prod).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public virtual bool UpdateStudent(student stu)
        {
            try
            {
                this.SaveOrUpdate(stu);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}