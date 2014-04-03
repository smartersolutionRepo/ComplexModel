using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
 
using System.Collections.ObjectModel;
 
using Omu.ValueInjecter;
using ComplexModelCore.Interfaces.Service;
using ComplexModelData;
using ComplexModelCore.Model;
namespace ComplexModelExample.Controllers
{
    public class studentController : BootstrapBaseController
    {
        private DataContext db = new DataContext();
        public IStudentService _Studentservice;
        //
        // GET: /student/

        public ActionResult Index()
        {
            _Studentservice = DependencyResolver.Current.GetService<IStudentService>();
            return View(_Studentservice.GetAll().ToList());
        }

        public ActionResult Details(int id = 0)
        {
            student students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        public ActionResult Create()
        {
            var _ProductService = DependencyResolver.Current.GetService<IProductService>();
            var _CustomerService = DependencyResolver.Current.GetService<ICustomerService>();
            ViewBag.Products = new MultiSelectList(_ProductService.GetAll(), "Id", "ProductName");
            ViewBag.customerID = new SelectList(_CustomerService.GetAll(), "Id", "Name");
            return View(new studentviewmodel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(studentviewmodel studentmodel)
        {
            _Studentservice = DependencyResolver.Current.GetService<IStudentService>();
            var _ProductService = DependencyResolver.Current.GetService<IProductService>();
            if (ModelState.IsValid)
            {
                student stu = new student();
                stu.studentclass = studentmodel.studentclass;
                stu.studentname = studentmodel.studentname;
                stu.Age = studentmodel.Age;
                stu.customerID = studentmodel.customerID;
             
                stu.products = new Collection<Product>();
                if (studentmodel.products != null && studentmodel.products.Count() > 0)
                {
                    for (var catp = 0; catp < studentmodel.products.Count(); catp++)
                    {
                        var pid = studentmodel.products[catp];
                        var product = _ProductService.GetAll().Where(p => p.Id == pid).FirstOrDefault();
                        stu.products.Add(product);
                    }
                }
               
                stu.InjectFrom<UnflatLoopValueInjection>(studentmodel);
                bool studnt = _Studentservice.AddStudent(stu);
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.Customers, "Id", "Name", studentmodel.customerID);

            return View(studentmodel);
        }



        public ActionResult Edit(int id = 0)
        {
            student student = db.Students.Find(id);
            studentviewmodel stuVM = new studentviewmodel();

            if (student != null)
            {
                ViewBag.Products = new MultiSelectList(db.Products, "Id", "ProductName");

                stuVM.studentname = student.studentname;
                stuVM.studentclass = student.studentclass;
                stuVM.customerID = student.customerID;
                stuVM.Age = student.Age;
                stuVM.Id = student.Id;

                if (student.products != null && student.products.Count() > 0)
                {
                    stuVM.products = new int[student.products.Count()];
                    var i = 0;
                    foreach (var innerItem in student.products)
                    {
                        stuVM.products[i] = Convert.ToInt32(innerItem.Id);
                        i++;
                    }

                }
            }
            else
            {
                return HttpNotFound();
            }

            return View(stuVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(studentviewmodel student)
        {
            if (ModelState.IsValid)
            {
                student stu = db.Students.Find(student.Id);
                db.Students.Remove(stu);
                db.SaveChanges();
                stu = new student();
                stu.studentname = student.studentname;
                stu.studentclass = student.studentclass;
                stu.Id = student.Id;
                stu.customerID = student.customerID;
                stu.Age = student.Age;
                stu.products = new Collection<Product>();
                if (student.products != null && student.products.Count() > 0)
                {
                    for (var stup = 0; stup < student.products.Count(); stup++)
                    {
                        var pid = student.products[stup];
                        var product = db.Products.Where(p => p.Id == pid).First();
                        stu.products.Add(product);

                    }


                }
                db.Students.Add(stu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        public ActionResult Delete(int id = 0)
        {
            student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
