using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrameManager : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private GameObject _notifyFrame;
    [SerializeField] private GameObject _innerFrame;
    [SerializeField] private TextMeshProUGUI _innerFrameText;
}
