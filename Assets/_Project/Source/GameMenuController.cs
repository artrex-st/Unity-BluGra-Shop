using Source.EventServices.GameEvents;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : BaseScreen
{
    [SerializeField] private Button _mainMenuBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private ScreenReference _mainMenuRef;
    [SerializeField] private ScreenReference _settingsRef;
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

        _settingsBtn.onClick.AddListener(HandlerSettingsClick);
        _mainMenuBtn.onClick.AddListener(HandlerMainMenuClick);
        _closeBtn.onClick.AddListener(HandlerCloseClick);
    }

    private void HandlerCloseClick()
    {
        ScreenService.UnLoadAdditiveSceneAsync(_thisScreenRef);
        _eventsService.Invoke(new RequestGameStateUpdateEvent(GameStates.GameRunning));
    }

    private void HandlerSettingsClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_settingsRef);
    }

    private void HandlerMainMenuClick()
    {
        ScreenService.LoadingSceneAsync(_mainMenuRef);
    }
}
