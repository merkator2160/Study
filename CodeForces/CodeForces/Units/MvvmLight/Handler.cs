using GalaSoft.MvvmLight.Messaging;
using System;

namespace CodeForces.Units.MvvmLight
{
    public class Handler
    {
        public Handler(IMessenger messenger)
        {
            messenger.Register<String>(this, Console.WriteLine);
        }
    }
}