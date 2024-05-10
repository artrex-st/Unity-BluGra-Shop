using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//TODO: I'm still need to improve this service, "unsubscribe" have a bug and you need to use "Unsubscribe" manually
//by some "dispose" or whatever function when the owner of event becomes inaccessible
//In the future i will improve this events to fix the bug and unsubscribe with a Foreach on functions like disable, dispose, destroy etc
public class EventsService : MonoBehaviour, IEventsService
{
    private readonly Dictionary<Type, List<object>> _eventListeners = new();

    public void Subscribe<T>(Action<T> callback) where T : IEvent
    {
        Type eventType = typeof(T);

        if (!_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType] = new List<object>();
        }

        _eventListeners[eventType].Add(callback);
    }

    public void Unsubscribe<T>(Action<T> callback) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_eventListeners.TryGetValue(eventType, out List<object> listener))
        {
            listener.Remove(callback);
        }
    }

    public void Invoke<T>(T eventData) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_eventListeners.ContainsKey(eventType))
        {
            List<object> eventListeners = _eventListeners[eventType].ToList(); //TODO: this is a temporary solution to avoid unsubscribe bug,
                                                                               //this code don't allow events unsubiscribe more that his self

            foreach (object handler in eventListeners)
            {
                if (handler is Action<T> castedHandler)
                {
                    castedHandler.Invoke(eventData);
                }
            }
        }
    }
}
