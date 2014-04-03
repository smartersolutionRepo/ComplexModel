using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using ComplexModelCore.Model;
using DotNetOpenAuth.Messaging;
using ComplexModelCore.Interfaces.Service;
using Omu.ValueInjecter;
using ComplexModelData;

namespace ComplexModelExample.Controllers
{
    public class CategoryController : BootstrapBaseController
    {
        private DataContext db = new DataContext();
        public ICategoryService _CategoryService;
        //
        // GET: /Category/

        public ActionResult Index()
        {
            _CategoryService = DependencyResolver.Current.GetService<ICategoryService>();
            return View(_CategoryService.GetAll().ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            var _ProductService = DependencyResolver.Current.GetService<IProductService>();
            ViewBag.Products = new MultiSelectList(_ProductService.GetAll(), "Id", "ProductName");
            return View(new CategoryViewModel());
        }

        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categorymodel)
        {
            if (ModelState.IsValid)
            {
                _CategoryService = DependencyResolver.Current.GetService<ICategoryService>();
               var _ProductService = DependencyResolver.Current.GetService<IProductService>();
                Category cat = new Category();
                cat.CategoryName = categorymodel.CategoryName;
                cat.CatDesc = categorymodel.CatDesc;
                cat.Products = new Collection<Product>();
                cat.InjectFrom<UnflatLoopValueInjection>(categorymodel);
                if (categorymodel.Products != null && categorymodel.Products.Count() > 0)
                {
                    for (var catp = 0; catp < categorymodel.Products.Count(); catp++)
                    {
                        var pid = categorymodel.Products[catp];
                        var product = _ProductService.GetAll().Where(p=>p.Id==pid).FirstOrDefault();
                        cat.Products.Add(product);
                    }
                }
                 
             
                bool CG = _CategoryService.AddCategory(cat);
                return RedirectToAction("Index");
            }

            return View(categorymodel);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = db.Categories.Find(id);
            CategoryViewModel catVM = new CategoryViewModel();

            if (category != null)
            {
                ViewBag.Products = new MultiSelectList(db.Products, "Id", "ProductName");

                catVM.CategoryName = category.CategoryName;
                catVM.CatDesc = category.CatDesc;
                catVM.Id = category.Id;

                if (category.Products != null && category.Products.Count() > 0)
                {
                    catVM.Products = new int[category.Products.Count()];
                    var i = 0;
                    foreach (var innerItem in category.Products)
                    {
                        catVM.Products[i] = Convert.ToInt32(innerItem.Id);
                        i++;
                    }
                     
                }
            }
            else
            {
                return HttpNotFound();
            }

            return View(catVM);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categorymodel)
        {
            _CategoryService = DependencyResolver.Current.GetService<ICategoryService>();
            var _ProductService = DependencyResolver.Current.GetService<IProductService>();

            if (ModelState.IsValid)
            {
                Category cat = db.Categories.Find(categorymodel.Id);
                db.Categories.Remove(cat);
                db.SaveChanges();
                cat=new Category();
               
                cat.CategoryName = categorymodel.CategoryName;
                cat.CatDesc = categorymodel.CatDesc;
                cat.Products = new Collection<Product>();
                cat.InjectFrom<UnflatLoopValueInjection>(categorymodel);
                if (categorymodel.Products != null && categorymodel.Products.Count() > 0)
                {
                    for (var catp = 0; catp < categorymodel.Products.Count(); catp++)
                    {
                        var pid = categorymodel.Products[catp];
                        var product = _ProductService.GetAll().Where(p => p.Id == pid).FirstOrDefault();
                        cat.Products.Add(product);
                    }
                }
              
             
                bool CG = _CategoryService.AddCategory(cat);
                return RedirectToAction("Index");
            }
            return View(categorymodel);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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