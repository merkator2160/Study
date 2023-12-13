using gRPCClientAssembly.Client;
using InterfaceAssembly;

IMyInterface inter = new MyClient();

while (true)
{
    var ret = inter.Test();
    Console.WriteLine(ret);

    var ret2 = inter.Test2();
    Console.WriteLine(ret2);

    Console.ReadKey();
}
