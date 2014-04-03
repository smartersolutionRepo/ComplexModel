using ComplexModelCore.Interfaces.Repositories;
using ComplexModelCore.Model;
using ComplexModelData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;



namespace ComplexModelData
{
    public class DataContext : DbContext,IDataContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany<Category>(s => s.Categories).WithMany(c => c.Products).Map(c =>
            {
                c.MapLeftKey("Product_id");
                c.MapRightKey("Category_id");
                c.ToTable("ProductAndCats");
            });


            modelBuilder.Entity<student>().HasMany<Product>(s => s.products).WithMany(c => c.Students).Map(c =>
            {
                c.MapLeftKey("student_id");
                c.MapRightKey("product_id");
                c.ToTable("studentAndPro");
            });


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<student> Students { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }


         public ObjectContext ObjectContext()
        {
            return ((IObjectContextAdapter)this).ObjectContext;
        }

        public virtual IDbSet<T> DbSet<T>() where T : DomainObject
        {
            return Set<T>();
        }

        public new DbEntityEntry Entry<T>(T entity) where T : DomainObject
        {
            return base.Entry(entity);
        }
         
    }
}