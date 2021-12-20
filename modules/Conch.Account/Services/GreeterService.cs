using Conch.Shared;
using Grpc.Core;

namespace Conch.Account.Services
{
    public abstract partial class GreeterBase
    {
        public virtual Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }
    }

    public class HelloRequest
    {
        public string Name { get; set; }
    }

    public class HelloReply
    {
        public string Message { get; set; }
    }
    public class GreeterService : GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = $"Hello {request.Name}" });
        }
    }
}
