using BuisinessLogic.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace BuisinessLogic.Infrastructure
{
    public sealed class ServiceLocator : IDisposable
    {
        private Boolean _disposed;
        private readonly IUnityContainer _container;


        public ServiceLocator()
        {
            _container = UnityConfig.GetConfiguredContainer();
        }
        ~ServiceLocator()
        {
            Dispose(false);
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public IAngleDeterminationService AngleService => _container.Resolve<IAngleDeterminationService>();


        // IDisposable ////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(Boolean disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _container?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}