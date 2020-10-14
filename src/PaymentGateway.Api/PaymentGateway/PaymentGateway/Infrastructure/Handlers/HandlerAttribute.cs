using System;

namespace PaymentGateway.Infrastructure.Handlers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HandlerAttribute : Attribute
    {
        public HandlerAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}