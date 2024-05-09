using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerView : MonoBehaviour
{
    private int _maskRunning;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private HealthPlayerView _healthPlayerView;
    [SerializeField] private WeaponView _weaponView;

    public FloatReactiveProperty ForceJumpUp = new();
    public FloatReactiveProperty ForceJumpLeft = new();
    public ReactiveCommand OnCollisionGroundCommand = new();
    public ReactiveCommand OnGetDamageCommand = new();
    public ReactiveCommand OnGetBulletCommand = new();
    public ReactiveCommand OnGetCrystalCommand = new();
    public ReactiveCommand OnGameOverCommand = new();
    public ReactiveCommand OnGetSunCommand = new();
    public HealthPlayerView HealthPlayerView => _healthPlayerView;
    public WeaponView WeaponView => _weaponView;
    public Vector3 Speed { get; set; }

    public void UpdateSpeed(Vector2 speed)
        => _rigidbody2D.velocity = speed;

    public void OnGetSun()
        => _rigidbody2D.excludeLayers = _maskRunning;

    public void OnEndTimerRunnning()
        => _rigidbody2D.excludeLayers = 0;

    private void Start()
    {
        _maskRunning = LayerMask.GetMask("Jump", "Tree");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_rigidbody2D.excludeLayers == 0 && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnGameOverCommand.Execute();
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Jump"))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var sun = collision.gameObject.GetComponent<SunView>();
        if (sun != null)
        {
            sun.SetActive(false);
            OnGetSunCommand.Execute();
        }

        var bullet = collision.gameObject.GetComponent<BulletView>();
        if (bullet != null && bullet.InMove == false)
        {
            bullet.SetActive(false);
            OnGetBulletCommand.Execute();
        }

        var crystal = collision.gameObject.GetComponent<CrystalView>();
        if (crystal != null)
        {
            crystal.SetActive(false);
            OnGetCrystalCommand.Execute();
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
        ManagerUniRx.Dispose(OnGetBulletCommand);
        ManagerUniRx.Dispose(OnGetCrystalCommand);
        ManagerUniRx.Dispose(OnGameOverCommand);
        ManagerUniRx.Dispose(OnGetSunCommand);
    }
}
