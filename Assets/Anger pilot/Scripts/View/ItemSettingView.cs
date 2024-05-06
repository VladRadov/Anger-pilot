using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSettingView : MonoBehaviour
{
    [SerializeField] private Button _changStateSetting;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _settingOn;
    [SerializeField] private Sprite _settingOff;

    protected virtual void InitializeStateItem() { }

    protected virtual void ChangeStateSetingItem() { }

    protected void SettingOn()
        => _icon.sprite = _settingOn;

    protected void SettingOff()
        => _icon.sprite = _settingOff;

    private void Start()
    {
        _changStateSetting.onClick.AddListener(ChangeStateSetingItem);
    }

    private void OnEnable()
        => InitializeStateItem();
}
