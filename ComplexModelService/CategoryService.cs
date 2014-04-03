
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

    public partial class CategoryService : BaseService<Category>, ICategoryService
    {
        protected new ICategoryRepository Repository;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository CategoryRepository)
            : base(unitOfWork)
        {
            base.Repository = Repository = CategoryRepository;
        }



        public bool AddCategory(Category cat)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var category = new Category();
                    category.CategoryName = cat.CategoryName;
                    category.CatDesc = cat.CatDesc;
                    category.Products = new List<Product>();
                    context.Categories.Add(category);
                    context.SaveChanges();
                    foreach (var item in cat.Products)
                    {
                        var prod = new Product();
                        prod.Id = item.Id;
                        prod.Quantity = item.Quantity;
                        prod.ProductName = item.ProductName;
                        prod.ProductDesc = item.ProductDesc;
                        category.Products.Add(prod);
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

        public bool UpdateCategory(Category cat)
        {
            try
            {
                this.SaveOrUpdate(cat);
                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}



