using Source.EventServices.GameEvents;
using UnityEngine;

public enum GameStates
{
    GameRunning,
    GamePaused,
    GameWaiting
}

public class GameDataService : MonoBehaviour, IGameDataService
{
    public GameStates CurrentGameState { get; private set; } = GameStates.GameWaiting;

    private IEventsService _eventsService;

    public void Initialize()
    {
        _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
        _eventsService.AddListener<RequestGameStateUpdateEvent>(HandlerRequestGameStateUpdateEvent, GetHashCode());
    }

    public void SetGameState(GameStates newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            Debug.Log($"<color=Yellow>Game State Not changed!</color>");
            return;
        }

        Debug.Log($"<color=Green>Game State changed from: {CurrentGameState} to {newGameState}!</color>");
        CurrentGameState = newGameState;
        _eventsService.Invoke(new ResponseGameStateUpdateEvent(CurrentGameState));
    }

    private void HandlerRequestGameStateUpdateEvent(RequestGameStateUpdateEvent e)
    {
        SetGameState(e.NewGameState);
    }

    private void OnDestroy()
    {
        _eventsService.RemoveListener<RequestGameStateUpdateEvent>(GetHashCode());
    }
}
