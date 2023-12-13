using Grpc.Core;
using gRPCClientAssembly.Protos;
using gRPCOtherAssembly.Protos;

namespace gRPCServerAssembly;

public class ServerService : ProtoService.ProtoServiceBase
{
    public override Task<ReturnValue> Test(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new ReturnValue { Value = "Server!" });
    }

    public override Task<ReturnValue> Test2(Id request, ServerCallContext context)
    {
        return Task.FromResult(new ReturnValue { Value = "Server2!" });
    }
}