namespace PaymentGateway.Infrastructure.Handlers
{
    public interface IHandlerManager
    {
        IHandler GetHandler(string paymentMethod);
    }
}