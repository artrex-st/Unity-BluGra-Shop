using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : BaseScreen
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _parentItemFrame;
    [SerializeField] private ItemFrameManager _itemFrame;

    private void Awake()
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

        _closeButton.onClick.AddListener(CloseButtonClickHandler);

        for (int i = 0; i < 25; i++)
        {
            ItemFrameManager itemFrame = Instantiate(_itemFrame, _parentItemFrame.transform);
        }
    }

    private void CloseButtonClickHandler()
    {
        ScreenService.UnLoadAdditiveSceneAsync(_thisScreenRef);
    }
}
