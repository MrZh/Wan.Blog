using System.Threading.Tasks;
using Wan.Release.Infrastructure.Base;

namespace Wan.Release.Infrastructure.Event
{
    public class BaseEventBus
    {
        public static void BaseSendEvent(BaseEvent envent)
        {
            Task.Run(() => envent.SentEvent());
        }

        public static async void AsyncBaseSendEvent(BaseEvent envent)
        {
            await Task.Run(() => envent.SentEvent());
        }

        public virtual void SendEvent(BaseEvent envent)
        {
            Task.Run(() => envent.SentEvent());
            //donothing
        }
    }
}
