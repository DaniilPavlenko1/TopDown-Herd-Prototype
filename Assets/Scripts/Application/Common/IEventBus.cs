using System;

namespace Application.Common
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent eventData);
        IDisposable Subscribe<TEvent>(Action<TEvent> handler);
    }
}
