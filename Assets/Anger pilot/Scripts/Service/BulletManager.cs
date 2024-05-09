using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int _currentCountBullet;
    private List<BulletView> _bullets;

    [SerializeField] private int _countBullet;
    [SerializeField] private BulletView _prefabBulletView;
    [SerializeField] private GameObject _bulletLine;

    public void Initialize()
    {
        _bullets = new List<BulletView>();
        _currentCountBullet = _countBullet;

        for (int i = 0; i < _currentCountBullet; i++)
        {
            var healthIcon = Instantiate(_prefabBulletView, _bulletLine.transform);
            _bullets.Add(healthIcon);
        }
    }

    public bool TryFire()
    {
        if (_currentCountBullet != 0)
        {
            _currentCountBullet -= 1;
            _bullets[_currentCountBullet].SetActiveIcon(false);

            return true;
        }

        return false;
    }

    public void AddBullet()
    {
        if (_currentCountBullet != _countBullet)
        {
            _bullets[_currentCountBullet].SetActiveIcon(true);
            _currentCountBullet += 1;
        }
    }
}
