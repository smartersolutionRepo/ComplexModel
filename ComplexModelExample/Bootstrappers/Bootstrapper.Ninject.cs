[assembly: WebActivator.PreApplicationStartMethod(typeof(ComplexModelExample.Bootstrappers.Bootstrapper), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(ComplexModelExample.Bootstrappers.Bootstrapper), "Stop")]

namespace ComplexModelExample.Bootstrappers
{
    using System;
    using System.Web;
    using System.Web.Http;
  
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

  
     
    using ComplexModelExample.DependencyInjection;
    using ComplexModelCore.Interfaces.Service;
    using ComplexModelService;
    using ComplexModelCore.Interfaces.Repositories;
    using ComplexModelData;

    public partial class Bootstrapper
    {
        private static readonly Ninject.Web.Common.Bootstrapper bootstrapper = new Ninject.Web.Common.Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Ninject.Web.Common.Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();
            kernel.Bind<IPersonService>().To<PersonService>().InRequestScope();
            kernel.Bind<IPersonRepository>().To<PersonRepository>().InRequestScope();


            //Register Product Services
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<IProductRepository>().To<ProductRepository>().InRequestScope();

            //Register Category Services
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InRequestScope();

            //Register Customer Services
            kernel.Bind<ICustomerService>().To<CustomerService>().InRequestScope();
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>().InRequestScope();

            //Register Student Services
            kernel.Bind<IStudentService>().To<StudentService>().InRequestScope();
            kernel.Bind<IStudentRepository>().To<StudentRepository>().InRequestScope();
        
           
        }
    }
}