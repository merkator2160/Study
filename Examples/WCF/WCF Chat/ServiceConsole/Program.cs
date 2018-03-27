using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceConsole.Chat;

namespace ServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            String str;
            
            Console.Write("Enter you name: ");
            String name = Console.ReadLine();
            Console.WriteLine();

            ChatClient proxy = new ChatClient(new InstanceContext(new Client()));
            proxy.Open();
            
            Console.WriteLine("User in chat:");
            foreach (var user in proxy.Join(name))
            {
                Console.WriteLine(user);
            }
            Console.WriteLine();
            
            do
            {
                str = Console.ReadLine();
                Console.WriteLine("{0}: {1}",name, str);
                
            } while (str != "exit");
            
            proxy.Leave();
            proxy.Close();
        }
    }
    
    internal class Client : IChatCallback
    {
        public void Receive(string name, string msg)
        {
            Console.WriteLine("{0}: {1}", name, msg);
        }

        public void UserEnter(string name)
        {
            Console.WriteLine("{0} is joined.", name);
        }

        public void UserLeave(string name)
        {
            Console.WriteLine("{0} leave us.", name);
        }
    }

    
}
