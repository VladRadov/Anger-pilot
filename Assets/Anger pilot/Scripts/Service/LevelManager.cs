using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LevelManager : MonoBehaviour
{
    private FrameMapController _frameMapController;
    private bool _isGameOver;
    private bool _isPause;

    [SerializeField] private TimerRunningView _timerRunningView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private Level _level;
    [SerializeField] private int _widthFrame;
    [SerializeField] private List<ItemShop> _backgrounds;
    [SerializeField] private FrameMapView _frameMapViewStar;
    [SerializeField] private Sprite _bgDay;
    [SerializeField] private Sprite _groundBase;
    [SerializeField] private Sprite _groundDay;
    [SerializeField] private Button _pause;
    [SerializeField] private PlaneView _planeView;

    public TimerRunningView TimerRunningView => _timerRunningView;
    public PauseView PauseView => _pauseView;
    public FrameMapController FrameMapController => _frameMapController;
    public bool IsGameOver => _isGameOver;
    public bool IsPause => _isPause;
    public PlaneView PlaneView => _planeView;
    public ReactiveCommand<Vector3> OnJumpInTreeCommand = new();

    public void Initialize(Transform player)
    {
        _frameMapController = new FrameMapController(_level, _widthFrame, player);
        _frameMapController.Initialize();
        SetBGFrameMaps();
        _isGameOver = false;
        _isPause = false;
        _pause.onClick.AddListener(() => { _pauseView.SetActive(true); });
    }

    public void SetPause(bool value)
        => _isPause = value;

    public void GameOver()
        => _isGameOver = true;

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

    public void SubscribeWolfsOnGameOver(ReactiveCommand gameOverCommand, Transform positionPlayer)
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
        {
            foreach (var wolf in frameMap.Wolfs)
                gameOverCommand.Subscribe(_ => { wolf.OnPlayerCollisionGround(positionPlayer); });
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
            if (frameMap.BGView.IsVisible == false)
                frameMap.BGView.CheckingFrameOnPassed(positionPlayer);
    }

    public void SearchNearTree(Vector3 positionPlayer)
    {
        float distance = -1;
        Vector3 positionTree = Vector3.zero;
        foreach (var frameMap in _frameMapController.FrameMapViews)
        {
            foreach (var tree in frameMap.Trees)
            {
                if (positionPlayer.x < tree.transform.position.x)
                {
                    var distanceTemp = Vector3.Distance(positionPlayer, tree.transform.position);
                    if (distance == -1 || distanceTemp < distance)
                    {
                        distance = distanceTemp;
                        positionTree = tree.transform.position;
                    }
                }
            }
        }
        OnJumpInTreeCommand.Execute(new Vector3(positionTree.x, 15, 0));
    }

    public void AddObjectsDisposable()
    {
        foreach (var frameMap in _frameMapController.FrameMapViews)
        {
            ManagerUniRx.AddObjectDisposable(frameMap.SystemInput.OnMouseDownCommand);
            ManagerUniRx.AddObjectDisposable(frameMap.BGView.OnInvisibleFrameMapCommand);
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnJumpInTreeCommand);
    }
}
