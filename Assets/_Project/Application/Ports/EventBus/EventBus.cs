using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Android.Types;
using UnityEngine;

namespace Project.Application.EventBus
{
    public class EventBus:IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public EventBus()
        {
            Debug.Log($"[{nameof(EventBus)}] Created");
        }
        
        public void Publish<TEvent>(TEvent @event)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
            foreach (var handler in handlers.Cast<Action<TEvent>>())
                handler(@event);
            Debug.Log($"[{nameof(EventBus)}] Publish - {typeof(TEvent)}");
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
            _handlers[typeof(TEvent)].Add(handler);
            Debug.Log($"[{nameof(EventBus)}] Subscribe - {nameof(handler.Target)}:{handler.Method.Name}@{typeof(TEvent)}");
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            if(!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
            _handlers[typeof(TEvent)].Remove(handler);
            Debug.Log($"[{nameof(EventBus)}] Unsubscribe - {nameof(handler.Target)}:{handler.Method.Name}@{typeof(TEvent)}");
        }
    }
}