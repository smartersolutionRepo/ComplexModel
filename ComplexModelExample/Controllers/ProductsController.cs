using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using ComplexModelCore.Model;
using ComplexModelCore.Interfaces.Service;
using Omu.ValueInjecter;
using ComplexModelCore.Model;
using ComplexModelData;

namespace ComplexModelExample.Controllers
{
    public class ProductsController : BootstrapBaseController
    {
        private DataContext db = new DataContext();

        public IProductService _ProductService;

        //
        // GET: /Products/

        public ActionResult Index()
        {
            _ProductService = DependencyResolver.Current.GetService<IProductService>();
            return View(_ProductService.GetAll().ToList());
        }

        //
        // GET: /Products/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            _ProductService = DependencyResolver.Current.GetService<IProductService>();
            var data = new Product();
            if (ModelState.IsValid)
            {
                data.InjectFrom<UnflatLoopValueInjection>(model);
                bool said = _ProductService.AddProduct(data);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Products/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            _ProductService = DependencyResolver.Current.GetService<IProductService>();
            var data = new Product();
            if (ModelState.IsValid)
            {
                data.InjectFrom<UnflatLoopValueInjection>(model);
                bool said = _ProductService.UpdateProduct(data);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Products/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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