using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LevelManager : MonoBehaviour
{
    private FrameMapController _frameMapController;

    [SerializeField] private TimerRunningView _timerRunningView;
    [SerializeField] private Level _level;
    [SerializeField] private int _widthFrame;
    [SerializeField] private List<ItemShop> _backgrounds;
    [SerializeField] private FrameMapView _frameMapViewStar;
    [SerializeField] private Sprite _bgDay;
    [SerializeField] private Sprite _groundBase;
    [SerializeField] private Sprite _groundDay;

    public TimerRunningView TimerRunningView => _timerRunningView;

    public void Initialize()
    {
        _frameMapController = new FrameMapController(_level, _widthFrame);
        _frameMapController.Initialize();
        SetBGFrameMaps();
    }

    public void VibrationIphone()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn == 1)
            Handheld.Vibrate();
    }

    public void SetPlayerSkin(SkinItem currentSkin)
        => _frameMapController.SetHealthSkin(currentSkin);

    public void SetBGFrameMaps()
    {
        var currentBG = _backgrounds.Find(bg => bg.Name == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentGround);
        _frameMapViewStar.BGView.SetSpriteBG(currentBG.Icon);
        _frameMapViewStar.SetSpriteGround(_groundBase);

        foreach (var frameMap in _frameMapController.FrameMapViews)
        {
            frameMap.BGView.SetSpriteBG(currentBG.Icon);
            frameMap.SetSpriteGround(_groundBase);
        }
    }

    public void SetBGFramMapsDay()
    {
        var currentBG = _backgrounds.Find(bg => bg.Name == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentGround);
        _frameMapViewStar.BGView.SetSpriteBG(_bgDay);
        _frameMapViewStar.SetSpriteGround(_groundDay);

        foreach (var frameMap in _frameMapController.FrameMapViews)
        {
            frameMap.BGView.SetSpriteBG(_bgDay);
            frameMap.SetSpriteGround(_groundDay);
        }
    }

    public void SubscribeOnMouseDown(Action<Unit> action)
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
            frameMap.SystemInput.OnMouseDownCommand.Subscribe(action);
    }

    public void SubscribeOnMouseUp(Action<Unit> action)
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
            frameMap.SystemInput.OnMouseUpCommand.Subscribe(action);
    }

    public void CheckingInvisibleFrameMaps(Vector3 positionPlayer)
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
            if(frameMap.BGView.IsVisible == false)
                frameMap.BGView.CheckingFrameOnPassed(positionPlayer);
    }

    public void AddObjectsDisposable()
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
        {
            ManagerUniRx.AddObjectDisposable(frameMap.SystemInput.OnMouseDownCommand);
            ManagerUniRx.AddObjectDisposable(frameMap.BGView.OnInvisibleFrameMapCommand);
        }
    }
}
