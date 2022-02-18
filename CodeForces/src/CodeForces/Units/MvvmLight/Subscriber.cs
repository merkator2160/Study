using GalaSoft.MvvmLight.Messaging;
using System;

namespace CodeForces.Units.MvvmLight
{
    public class Subscriber
    {
        public Subscriber(IMessenger messenger)
        {
            messenger.Register<String>(this, Console.WriteLine);
        }
    }
}