using System;
using Fiver.Azure.ServiceBus.Common;

namespace Fiver.Azure.ServiceBus.Subscription
{
    public interface IAzureSubscriptionReceiver<T>
    {
        void Receive(
            Func<T, MessageProcessResponse> onProcess, 
            Action<Exception> onError, 
            Action onWait);
    }
}