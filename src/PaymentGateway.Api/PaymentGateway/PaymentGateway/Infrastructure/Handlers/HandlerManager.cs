using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Deposit.PG.Handler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Infrastructure.Handlers
{
    public class HandlerManager : IHandlerManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HandlerManager> _logger;
        private readonly List<Type> _handlers = new List<Type>();

        public HandlerManager(IServiceProvider serviceProvider,
            ILogger<HandlerManager> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            //TODO: Add pg handlers here
            _handlers.Add(typeof(QFPayHandler));
        }

        public IHandler GetHandler(string paymentMethod)
        {
            foreach (var impl in _handlers)
            {
                var attributes = impl.GetCustomAttributes<HandlerAttribute>(true);

                if (attributes.Where(attr => attr != null)
                    .All(attr => !string.Equals(attr.Name, GetHandlerName(paymentMethod), StringComparison.CurrentCultureIgnoreCase))) continue;

                return (IHandler)ActivatorUtilities.CreateInstance(_serviceProvider, impl);
            }

            _logger.LogError($"Could not find a IHandler implementation for this serviceName: {paymentMethod}");
            throw new HandlerException($"No handler for {paymentMethod} request");
        }

        private string GetHandlerName(string paymentMethod)
        {
            string handlerName = paymentMethod;
            switch (paymentMethod)
            {
                case "61":
                    handlerName = "QFPay";
                    break;
                case "71":
                    handlerName = "WireCard";
                    break;
            }
            return handlerName;
        }
    }
}