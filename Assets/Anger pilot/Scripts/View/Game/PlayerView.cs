using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private HealthPlayerView _healthPlayerView;
    [SerializeField] private WeaponView _weaponView;

    public FloatReactiveProperty ForceJumpUp = new();
    public FloatReactiveProperty ForceJumpLeft = new();
    public ReactiveCommand OnCollisionGroundCommand = new();
    public ReactiveCommand OnGetDamageCommand = new();
    public HealthPlayerView HealthPlayerView => _healthPlayerView;
    public WeaponView WeaponView => _weaponView;

    public void UpdateSpeed(Vector2 speed)
        => _rigidbody2D.velocity = speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnCollisionGroundCommand.Execute();

        var deadlyPlaceEnemyView = collision.collider.GetComponent<DeadlyPlaceEnemyView>();
        if (deadlyPlaceEnemyView != null)
            deadlyPlaceEnemyView.SetActiveEnemy(false);

        var enemy = collision.collider.GetComponent<EnemyView>();
        if (enemy != null)
        {
            enemy.SetActive(false);
            OnGetDamageCommand.Execute();
        }
    }

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_healthPlayerView == null)
            _healthPlayerView = GetComponent<HealthPlayerView>();

        if (_weaponView == null)
            _weaponView = GetComponent<WeaponView>();
    }

    private void OnDisable()
    {
        ManagerUniRx.Dispose(ForceJumpUp);
        ManagerUniRx.Dispose(ForceJumpLeft);
        ManagerUniRx.Dispose(OnCollisionGroundCommand);
        ManagerUniRx.Dispose(OnGetDamageCommand);
    }
}
