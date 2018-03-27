using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;



namespace RequestService.Models
{
    public class RequestServiceInstanceProvider : IInstanceProvider
    {
        public RequestServiceInstanceProvider(Type type)
        {
            ServiceType = type;
            Container = new UnityContainer();
        }
        public RequestServiceInstanceProvider() : this(null)
        {

        }



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public UnityContainer Container { set; get; }
        public Type ServiceType { set; get; }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public object GetInstance(InstanceContext instanceContext)
        {
            return Container.Resolve(ServiceType);
        }
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return Container.Resolve(ServiceType);
        }
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

        }
    }
}
