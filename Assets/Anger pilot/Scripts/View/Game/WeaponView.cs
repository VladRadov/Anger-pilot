using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private BulletView _prefabBullet;

    public void Fire()
    {
        var bullet = PoolObjects<BulletView>.GetObject(_prefabBullet);
        bullet.SetLocalPosition(transform.position);
        bullet.StartMoveBullet();
        StartCoroutine(bullet.Lifetime());
    }
}
