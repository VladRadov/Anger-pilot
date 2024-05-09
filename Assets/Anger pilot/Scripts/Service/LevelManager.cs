using System;
using UnityEngine;
using UniRx;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private int _widthFrame;

    private FrameMapController _frameMapController;

    public void Initialize()
    {
        _frameMapController = new FrameMapController(_level, _widthFrame);
        _frameMapController.Initialize();
    }

    public void SetPlayerSkin(SkinItem currentSkin)
        => _frameMapController.SetHealthSkin(currentSkin);

    public void SubscribeOnInputSystem(Action<Unit> action)
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
            frameMap.SystemInput.OnMouseDownCommand.Subscribe(action);
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
