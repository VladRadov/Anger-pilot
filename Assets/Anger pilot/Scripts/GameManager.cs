using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour
{
    [Header("Services")]
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private BulletManager _bulletManager;
    [Header("Views")]
    [SerializeField] private PlayerView _playerView;
    [Header("UI")]
    [SerializeField] private Button _fire;

    private PlayerController _playerController;
    private Player _player;

    private void Start()
    {
        _levelManager.Initialize();
        _healthManager.Initialize();
        _bulletManager.Initialize();

        _levelManager.SetPlayerSkin(_healthManager.CurrentSkin);

        _player = new Player();
        _playerController = new PlayerController(_playerView, _player);
        _playerController.Initialize();

        _fire.onClick.AddListener(() => { if(_bulletManager.TryFire()) _playerView.WeaponView.Fire(); });

        _levelManager.SubscribeOnInputSystem(_ => { _player.Jump(Vector2.up * _playerView.ForceJumpUp.Value + Vector2.right * _playerView.ForceJumpLeft.Value); });

        _playerView.OnCollisionGroundCommand.Subscribe(_ => { _player.OnCollisionGround(); });
        _playerView.HealthPlayerView.OnGetHelthCommand.Subscribe(_ => { _healthManager.AddHealth(); });
        _playerView.OnGetDamageCommand.Subscribe(_ => { _healthManager.Damage(); });

        _player.Speed.Subscribe(_ => { _levelManager.CheckingInvisibleFrameMaps(_playerView.transform.position); });
        AddObjectsDisposable();
    }

    private void AddObjectsDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_playerView.HealthPlayerView.OnGetHelthCommand);
        ManagerUniRx.AddObjectDisposable(_player.Speed);
        ManagerUniRx.AddObjectDisposable(_playerView.OnCollisionGroundCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetDamageCommand);
        _levelManager.AddObjectsDisposable();
    }
}
