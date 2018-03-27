using System;
using log4net.Config;
using Microsoft.Practices.Unity;
using RequestService.Common.Interfaces;
using RequestService.DataLayer;
using UnityLog4NetExtension.Log4Net;


namespace RequestService
{
	public sealed class UnityFactory
	{
	    private static IUnityContainer _container;
	    private static Boolean _isConfigured;



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
	    public static IUnityContainer GetInstance()
	    {
	        if (_container == null)
	        {
	            _container = new UnityContainer();
	        }

            if (!_isConfigured)
            {
                //container.LoadConfiguration();    // AppConfig
                DOMConfigurator.Configure();

                _container.RegisterType<RequestService>();
                _container.RegisterType<IDataProvider, DbDataProvider>();
                _container.AddNewExtension<Log4NetExtension>();

                _isConfigured = true;
            }

	        return _container;
	    }
    }    
}