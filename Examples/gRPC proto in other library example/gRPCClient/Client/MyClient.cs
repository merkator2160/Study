using Grpc.Net.Client;
using gRPCClientAssembly.Protos;
using gRPCOtherAssembly.Protos;
using InterfaceAssembly;

namespace gRPCClientAssembly.Client;

public sealed class MyClient: IMyInterface
{
    private readonly ProtoService.ProtoServiceClient _Channel;

    public MyClient()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5238/");
        _Channel = new ProtoService.ProtoServiceClient(channel);
    }

    public string Test()
    {
        return _Channel.Test(new Empty()).Value;
    }

    public string Test2()
    {
        return _Channel.Test2(new Id { Id_ = 1 , Toto = ""}).Value;
    }
}