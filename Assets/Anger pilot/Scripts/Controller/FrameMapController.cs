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
    private Transform _player;

    public FrameMapController(Level level, int widthFrame, Transform player)
    {
        _level = level;
        _frameMapViews = new();
        _widthFrame = widthFrame;
        _player = player;
    }

    public List<FrameMapView> FrameMapViews => _frameMapViews;

    public void SetActiveEnemyes(bool value)
    {
        foreach (var frame in _frameMapViews)
            frame.SetActiveWols(value);

        PoolObjects<BatView>.SetActiveObjects(value);
    } 

    private Vector3 NextPositionFrameMap => new Vector3(_lastFrameMap.LocalPosition.x + _widthFrame, 0, 0);

    private void CreateHealth(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 1);
        if (indexRange == 0)
        {
            var health = PoolObjects<HealthView>.GetObject(frameMapView.PrefabHealth);
            var positionX = Random.Range(-_widthFrame, _widthFrame);
            health.SetLocalPosition(new Vector3(frameMapView.transform.position.x + positionX, 0, 0));
            health.SetSkin(_currentSkin.Icon);
        }
    }

    private void CreateEnemy(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 2);
        if (indexRange == 1)
        {
            var bat = PoolObjects<BatView>.GetObject(_level.PrefabBat);
            bat.SetTransformPlayer(_player);
            var positionX = Random.Range(-_widthFrame, _widthFrame);
            bat.SetLocalPosition(new Vector3(frameMapView.transform.position.x + positionX, 0, 0));
        }
    }

    private void CreateBullet(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 4);
        if (indexRange == 2)
        {
            var bullet = PoolObjects<BulletView>.GetObject(_level.PrefabBullet);
            var positionX = Random.Range(-_widthFrame, _widthFrame);
            bullet.SetLocalPosition(new Vector3(frameMapView.transform.position.x + positionX, 0, 0));
            bullet.SetMove(false);
        }
    }

    private void CreateCrystal(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 4);
        if (indexRange == 2)
        {
            var bullet = PoolObjects<CrystalView>.GetObject(_level.PrefabCrystal);
            var positionX = Random.Range(-_widthFrame, _widthFrame);
            bullet.SetLocalPosition(new Vector3(frameMapView.transform.position.x + positionX, 0, 0));
        }
    }

    private void RandomCreateSun(FrameMapView frameMapView)
    {
        var indexRange = Random.Range(0, 3);
        if (indexRange == 2)
            CreateSun(frameMapView);
    }

    private void CreateSun(FrameMapView frameMapView)
    {
        var sun = PoolObjects<SunView>.GetObject(_level.PrefabSun);
        var positionX = Random.Range(-_widthFrame, _widthFrame);
        sun.SetLocalPosition(new Vector3(frameMapView.transform.position.x + positionX, 0, 0));
    }

    public void SetHealthSkin(SkinItem currentSkin)
        => _currentSkin = currentSkin;

    public void Initialize()
    {
        for(int i = 0; i < _level.PrefabsFrameMapView.Count; i++)
        {
            var frameMap = Instantiate(_level.PrefabsFrameMapView[i]);

            if (i == 2)
                CreateSun(frameMap);

            if (i % 2 == 0)
                frameMap.transform.Rotate(0, 180, 0);
            else
                frameMap.RotationWolfs();

            if (_frameMapViews.Count != 0)
                frameMap.SetLocalPosition(NextPositionFrameMap);

            frameMap.SetWidthFrame(_widthFrame);
            _frameMapViews.Add(frameMap);
            _lastFrameMap = frameMap;

            CreateEnemy(frameMap);
            CreateBullet(frameMap);
            CreateCrystal(frameMap);
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
        CreateBullet(frameMapView);
        CreateCrystal(frameMapView);
        RandomCreateSun(frameMapView);
        _lastFrameMap = frameMapView;
    }
}
