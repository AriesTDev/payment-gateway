using System.Threading.Tasks;

namespace PaymentGateway.Contracts
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
