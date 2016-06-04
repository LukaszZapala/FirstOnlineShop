using AlkusStore.Repository.Abstract;
using AlkusStore.Repository.Concrete;
//using AlkusStore.Repository.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AlkusStore.Infrastructure
{
    class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IShoppingCartRepository>().To<ShoppingCartRepository>();
            kernel.Bind<IOrdersRepository>().To<OrdersRepository>();
            kernel.Bind<IAdminRepository>().To<AdminRepository>();
        }
    }
}
