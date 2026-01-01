using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Application.EventBus
{
    public class EventBus:IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();
        
        public void Publish<TEvent>(TEvent @event)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
            foreach (var handler in handlers.Cast<Action<TEvent>>())
                handler(@event);
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
            _handlers[typeof(TEvent)].Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            if(!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
            _handlers[typeof(TEvent)].Remove(handler);
        }
    }
}