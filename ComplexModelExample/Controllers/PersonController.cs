using ComplexModelCore.Interfaces.Service;
using ComplexModelCore.Model;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplexModelExample.Controllers
{
    public partial class PersonController : Controller
    {
        
        public IPersonService _PersonService;


        public PersonController()
        {
            

        }

        //public ActionResult Index()
        //{
        //    return View(db.Persons.ToList());
        //}

        public ActionResult Create()
        {
            return View(new PersonVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonVM model)
        {
            _PersonService = DependencyResolver.Current.GetService<IPersonService>();
            var data = new Person();
            if (ModelState.IsValid)
            {
                data.InjectFrom<UnflatLoopValueInjection>(model);
                bool said = _PersonService.AddPerson(data);
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
