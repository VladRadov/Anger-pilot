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
    [SerializeField] private SpriteRenderer _head;

    public FloatReactiveProperty ForceJumpUp = new();
    public FloatReactiveProperty ForceJumpLeft = new();
    public ReactiveCommand OnCollisionPlaceJumpCommand = new();
    public ReactiveCommand OnGetDamageCommand = new();
    public ReactiveCommand OnGetBulletCommand = new();
    public ReactiveCommand OnGetCrystalCommand = new();
    public ReactiveCommand OnGameOverCommand = new();
    public ReactiveCommand OnGetSunCommand = new();
    public ReactiveCommand OnCollisionGroundCommand = new();
    public HealthPlayerView HealthPlayerView => _healthPlayerView;
    public WeaponView WeaponView => _weaponView;
    public Vector3 Speed { get; set; }

    public void UpdateSpeed(Vector2 speed)
        => _rigidbody2D.velocity = speed;

    public void OnGetSun()
        => _rigidbody2D.excludeLayers = _maskRunning;

    public void OnEndTimerRunnning()
        => _rigidbody2D.excludeLayers = 0;

    public void SetSpriteHead(Sprite spriteHead)
        => _head.sprite = spriteHead;

    private void Start()
    {
        _maskRunning = LayerMask.GetMask("Jump", "Tree");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnCollisionGroundCommand.Execute();

        if (_rigidbody2D.excludeLayers == 0 && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnGameOverCommand.Execute();
            return;
        }

        var deadlyPlaceEnemyView = collision.collider.GetComponent<DeadlyPlaceEnemyView>();
        if (deadlyPlaceEnemyView != null)
        {
            deadlyPlaceEnemyView.SetActiveEnemy(false);
            OnCollisionPlaceJumpCommand.Execute();
        }

        var enemy = collision.collider.GetComponent<EnemyView>();
        if (enemy != null)
        {
            enemy.SetActive(false);
            OnGetDamageCommand.Execute();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (/*_rigidbody2D.velocity == Vector2.zero &&*/ collision.gameObject.layer == LayerMask.NameToLayer("Jump"))
            OnCollisionPlaceJumpCommand.Execute();
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

    private void OnBecameInvisible()
    {
        if(OnGameOverCommand.IsDisposed == false)
            OnGameOverCommand.Execute();
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
        ManagerUniRx.Dispose(OnCollisionPlaceJumpCommand);
        ManagerUniRx.Dispose(OnGetDamageCommand);
        ManagerUniRx.Dispose(OnGetBulletCommand);
        ManagerUniRx.Dispose(OnGetCrystalCommand);
        ManagerUniRx.Dispose(OnGameOverCommand);
        ManagerUniRx.Dispose(OnGetSunCommand);
        ManagerUniRx.Dispose(OnCollisionGroundCommand);
    }
}
