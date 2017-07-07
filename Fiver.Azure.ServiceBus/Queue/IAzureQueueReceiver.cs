using Fiver.Azure.ServiceBus.Common;
using System;

namespace Fiver.Azure.ServiceBus.Queue
{
    public interface IAzureQueueReceiver<T>
    {
        void Receive(
            Func<T, MessageProcessResponse> onProcess,
            Action<Exception> onError,
            Action onWait);
    }
}
