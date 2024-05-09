using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private BulletView _prefabBullet;

    public ReactiveCommand OnFireCommand = new();

    public void Fire()
    {
        var bullet = PoolObjects<BulletView>.GetObject(_prefabBullet);
        bullet.SetLocalPosition(transform.position);
        bullet.StartMoveBullet();
        bullet.SetMove(true);
        StartCoroutine(bullet.Lifetime());

        OnFireCommand.Execute();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnFireCommand);
    }
}
