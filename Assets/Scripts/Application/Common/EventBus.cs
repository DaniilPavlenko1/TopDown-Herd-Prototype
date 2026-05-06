using System;
using System.Collections.Generic;

namespace Application.Common
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public void Publish<TEvent>(TEvent eventData)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out List<Delegate> handlers))
                return;

            Delegate[] snapshot = handlers.ToArray();

            for (int i = 0; i < snapshot.Length; i++)
            {
                if (snapshot[i] is Action<TEvent> handler)
                    handler(eventData);
            }
        }

        public IDisposable Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out List<Delegate> handlers))
            {
                handlers = new List<Delegate>();
                _handlers.Add(typeof(TEvent), handlers);
            }

            handlers.Add(handler);

            return new Subscription(() => Unsubscribe(typeof(TEvent), handler));
        }

        private void Unsubscribe(Type eventType, Delegate handler)
        {
            if (!_handlers.TryGetValue(eventType, out List<Delegate> handlers))
                return;

            handlers.Remove(handler);

            if (handlers.Count == 0)
                _handlers.Remove(eventType);
        }

        private sealed class Subscription : IDisposable
        {
            private Action _disposeAction;

            public Subscription(Action disposeAction)
            {
                _disposeAction = disposeAction;
            }

            public void Dispose()
            {
                _disposeAction?.Invoke();
                _disposeAction = null;
            }
        }
    }
}
