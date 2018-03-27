using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using ChatService;


namespace ServiceServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Chat));
            host.Open();
            
            Console.WriteLine("Server is up...");

            #region Output dispatchers listening

            foreach (Uri uri in host.BaseAddresses)
            {
                Console.WriteLine("\t{0}", uri.ToString());
            }
            
            Console.WriteLine();
            Console.WriteLine("Number of dispatchers listening : {0}", host.ChannelDispatchers.Count);
            
            foreach (ChannelDispatcher dispatcher in host.ChannelDispatchers)
            {
                Console.WriteLine("\t{0}, {1}", dispatcher.Listener.Uri.ToString(), dispatcher.BindingName);
            }

            #endregion

            Console.ReadKey();

            host.Close();
        }
    }
}
