using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
internal struct GameEventListened
{
    public int ListenerHashCode;
    public Action<GameEvent> GameEvent;

    public GameEventListened(int listenerHashCode, Action<GameEvent> gameEvent)
    {
        ListenerHashCode = listenerHashCode;
        GameEvent = gameEvent;
    }
}

public class EventsService : BaseService, IEventsService
{
    private Dictionary<int, List<GameEventListened>> _eventListeners = new Dictionary<int, List<GameEventListened>>();

    public override void Setup()
    {
        ServiceLocator.Instance.RegisterService<IEventsService>(this);
    }

    public void AddListener<T>(Action<T> action, int listenerHashCode) where T : GameEvent
    {
        int actionTypeHashCode = typeof(T).GetHashCode();
        Action<GameEvent> castedGameEvent = myEvent => action((T)myEvent);
        GameEventListened gameEventAndListener = new GameEventListened(listenerHashCode, castedGameEvent);

        if (_eventListeners.ContainsKey(actionTypeHashCode))
        {
            _eventListeners[actionTypeHashCode].Add(gameEventAndListener);
            return;
        }

        _eventListeners.Add(actionTypeHashCode, new List<GameEventListened>() { gameEventAndListener });
    }

    public void RemoveListener<T>(int listenerHashCode) where T : GameEvent
    {
        int hashCode = typeof(T).GetHashCode();
        if (_eventListeners.ContainsKey(hashCode))
        {
            RemoveEvent(hashCode, listenerHashCode);

            if (_eventListeners[hashCode].Count < 1)
            {
                RemoveEventReferences(hashCode);
            }

            return;
        }
    }

    public void Invoke(GameEvent eventData)
    {
        int hashCode = eventData.GetType().GetHashCode();

        if (_eventListeners.ContainsKey(hashCode))
        {
            foreach (GameEventListened action in _eventListeners[hashCode])
            {
                Debug.Log("Invoked one");
                action.GameEvent?.Invoke(eventData);
            }
            return;
        }
    }

    private void RemoveEvent(int eventHash, int listenerHashCode)
    {
        foreach (GameEventListened gameEventByHash in _eventListeners[eventHash])
        {
            if(gameEventByHash.ListenerHashCode == listenerHashCode)
            {
                _eventListeners[eventHash].Remove(gameEventByHash);
                return;
            }
        }
    }

    private void RemoveEventReferences(int eventHash)
    {
        _eventListeners.Remove(eventHash);
    }
}
