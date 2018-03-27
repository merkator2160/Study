using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;



namespace RequestService.Models
{
    public class RequestServiceHost : ServiceHost
    {
        public RequestServiceHost() : base()
        {
            Container = new UnityContainer();
        }
        public RequestServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {
            Container = new UnityContainer();
        }
        public RequestServiceHost(Type serviceType, UnityContainer container, params Uri[] baseAddresses): base(serviceType, baseAddresses)
        {
            Container = container;
        }



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public UnityContainer Container { set; get; }



        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        protected override void OnOpening()
        {
            if (Description.Behaviors.Find<RequestServiceBehavior>() == null)
            {
                Description.Behaviors.Add(new RequestServiceBehavior(Container));
            }
    
            base.OnOpening();
        }





    }
}
