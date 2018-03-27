using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;



        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //var mock = new Mock<IGameRepository>();
            //mock.Setup(m => m.Games).Returns(new List<Game>
            //{
            //    new Game { Name = "SimCity", Price = 1499 },
            //    new Game { Name = "TITANFALL", Price = 2299 },
            //    new Game { Name = "Battlefield 4", Price = 899.4M }
            //});

            //_kernel.Bind<IGameRepository>().ToConstant(mock.Object);

            _kernel.Bind<IGameRepository>().To<EFGameRepository>();
        }
    }
}