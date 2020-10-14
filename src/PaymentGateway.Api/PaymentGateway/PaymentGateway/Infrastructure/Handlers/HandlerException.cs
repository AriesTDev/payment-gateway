using System;
using System.Runtime.Serialization;

namespace PaymentGateway.Infrastructure.Handlers
{
    public class HandlerException : ApplicationException
    {
        public HandlerException() : base() { }
        public HandlerException(string message) : base(message) { }

        public HandlerException(string message, Exception innerException) : base(message, innerException) { }
        public HandlerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}