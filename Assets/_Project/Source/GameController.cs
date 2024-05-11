using Source.EventServices.GameEvents;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseScreen
{
    [SerializeField] private Button _mainMenuBtn;
    [SerializeField] private ScreenReference _gameMenuRef;
    private IEventsService _eventsService;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Dispose();
    }

    private new void Initialize()
    {
        base.Initialize();
        _eventsService = ServiceLocator.Instance.GetService<IEventsService>();

        _mainMenuBtn.onClick.AddListener(HandlerGameMenuClick);
        StartGame();
    }

    private async void StartGame()
    {
        //TODO: Finish Loading screen fade effect time using DOTween
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        _eventsService.Invoke(new RequestGameStateUpdateEvent(GameStates.GameRunning));
    }

    private void HandlerGameMenuClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_gameMenuRef);
        _eventsService.Invoke(new RequestGameStateUpdateEvent(GameStates.GamePaused));
    }
}
