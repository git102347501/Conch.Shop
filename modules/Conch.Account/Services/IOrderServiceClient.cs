using Conch.Shared;

namespace Conch.Account.Services
{
    public interface IOrderServiceClient
    {
        Task<HelloRequest> SubmitOrder();
    }
}
