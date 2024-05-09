using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FrameMapController : MonoBehaviour
{
    private Level _level;
    private List<FrameMapView> _frameMapViews;
    private int _widthFrame;
    private FrameMapView _lastFrameMap;
    private SkinItem _currentSkin;

    public FrameMapController(Level level, int widthFrame)
    {
        _level = level;
        _frameMapViews = new();
        _widthFrame = widthFrame;
    }

    public List<FrameMapView> FrameMapViews => _frameMapViews;

    private Vector3 NextPositionFrameMap => new Vector3(_lastFrameMap.LocalPosition.x + _widthFrame, 0, 0);

    private void CreateHealth(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 4);
        if (indexRange == 2)
        {
            var health = PoolObjects<HealthView>.GetObject(frameMapView.PrefabHealth, frameMapView.transform);
            var positionX = Random.Range(-_widthFrame, _widthFrame);
            health.SetLocalPosition(new Vector3(positionX, 0, 0));
            health.SetSkin(_currentSkin.Icon);
        }
    }

    private void CreateEnemy(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 3);
        if (indexRange == 2)
        {
            var bat = PoolObjects<EnemyView>.GetObject(_level.PrefabBat, frameMapView.transform);
            var positionX = Random.Range(-_widthFrame, _widthFrame);
            var positionY = Random.Range(-10, 17);
            bat.SetLocalPosition(new Vector3(positionX, positionY, 0));
        }
    }

    public void SetHealthSkin(SkinItem currentSkin)
        => _currentSkin = currentSkin;

    public void Initialize()
    {
        for(int i = 0; i < _level.CountFrame; i++)
        {
            var frameMap = Instantiate(_level.PrefabFrameMapView);

            if (_frameMapViews.Count != 0)
                frameMap.SetLocalPosition(NextPositionFrameMap);

            frameMap.SetWidthFrame(_widthFrame);
            _frameMapViews.Add(frameMap);
            _lastFrameMap = frameMap;
        }

        SubscribeOnInvisibleFrameMap();
    }

    private void SubscribeOnInvisibleFrameMap()
    {
        foreach (var frameMap in _frameMapViews)
            frameMap.BGView.OnInvisibleFrameMapCommand.Subscribe(frameMapView => { OnInvisibleFrameMap(frameMapView); });
    }

    private void OnInvisibleFrameMap(FrameMapView frameMapView)
    {
        frameMapView.SetLocalPosition(NextPositionFrameMap);
        CreateHealth(frameMapView);
        CreateEnemy(frameMapView);
        _lastFrameMap = frameMapView;
    }
}
