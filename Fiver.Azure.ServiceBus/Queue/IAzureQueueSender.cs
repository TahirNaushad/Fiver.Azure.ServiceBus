using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiver.Azure.ServiceBus.Queue
{
    public interface IAzureQueueSender<T>
    {
        Task SendAsync(T item);
        Task SendAsync(T item, Dictionary<string, object> properties);
    }
}
