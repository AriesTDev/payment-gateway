using System.Threading.Tasks;

namespace Aitea.PaymentGateway.Contracts
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
