using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;



namespace RequestService.Models
{
    public class RequestServiceBehavior : IServiceBehavior
    {
        private ServiceHost _serviceHost = null;



        public RequestServiceBehavior()
        {
            InstanceProvider = new RequestServiceInstanceProvider();
        }
        public RequestServiceBehavior(UnityContainer container)
        {
            InstanceProvider = new RequestServiceInstanceProvider
            {
                Container = container
            };
        }



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public RequestServiceInstanceProvider InstanceProvider { get; set; }




        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
                {
                    ChannelDispatcher cd = cdb as ChannelDispatcher;
                    if (cd != null)
                    {
                        foreach (EndpointDispatcher ed in cd.Endpoints)
                        {
                            InstanceProvider.ServiceType = serviceDescription.ServiceType;
                            ed.DispatchRuntime.InstanceProvider = InstanceProvider;
                        }
                    }
                }
        }


    }
}
