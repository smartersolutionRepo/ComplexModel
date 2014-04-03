using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplexModelCore.Model;
using ComplexModelCore.Interfaces.Service;
using Omu.ValueInjecter;
using ComplexModelData;

namespace ComplexModelExample.Controllers
{
    public class customerController : Controller
    {
        private DataContext db = new DataContext();
        public ICustomerService _Customerservice;
        //
        // GET: /customer/

        public ActionResult Index()
        {
            _Customerservice = DependencyResolver.Current.GetService<ICustomerService>();
            return View(_Customerservice.GetAll().ToList());
        }

        //
        // GET: /customer/Details/5

        public ActionResult Details(int id = 0)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        //
        // GET: /customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /customer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customers model)
        {
            _Customerservice = DependencyResolver.Current.GetService<ICustomerService>();
            var data = new Customers();
            if (ModelState.IsValid)
            {
                data.InjectFrom<UnflatLoopValueInjection>(model);
                bool cus = _Customerservice.AddCustomer(data);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        //
        // POST: /customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customers model)
        {
            _Customerservice = DependencyResolver.Current.GetService<ICustomerService>();
            var data = new Customers();
            if (ModelState.IsValid)
            {
                data.InjectFrom<UnflatLoopValueInjection>(model);
                bool cus = _Customerservice.UpdateCustomer(data);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        //
        // POST: /customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}