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
    [SerializeField] private ScoreManager _scoreManager;
    [Header("Views")]
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private GameOverView _gameOverView;
    [Header("UI")]
    [SerializeField] private Button _fire;

    private PlayerController _playerController;
    private Player _player;

    private void Start()
    {
        AudioManager.Instance.PlayMusicGame();
        _levelManager.Initialize();
        _healthManager.Initialize();
        _bulletManager.Initialize();

        _levelManager.SetPlayerSkin(_healthManager.CurrentSkin);

        _player = new Player();
        _playerController = new PlayerController(_playerView, _player);
        _playerController.Initialize();
        _playerView.Speed = Vector2.up * _playerView.ForceJumpUp.Value + Vector2.right * _playerView.ForceJumpLeft.Value;

        _fire.onClick.AddListener(() => { if (_bulletManager.TryFire()) _playerView.WeaponView.Fire(); });

        _levelManager.SubscribeOnMouseDown(_ => {if(_player.IsStandGround) _playerController.SetMouseHolding(true); });
        _levelManager.SubscribeOnMouseUp(_ => { _playerController.SetMouseHolding(false); });

        _playerView.OnGetCrystalCommand.Subscribe(_ => { _scoreManager.AddCrystal(); _scoreManager.UpdateScrystal(); });
        _playerView.OnGetCrystalCommand.Subscribe(_ => { AudioManager.Instance.PlayGetCrystal(); });
        _playerView.OnGetBulletCommand.Subscribe(_ => { _bulletManager.AddBullet(); });
        _playerView.OnCollisionGroundCommand.Subscribe(_ => { _player.OnCollisionGround(); });
        _playerView.OnCollisionGroundCommand.Subscribe(_ => { _scoreManager.AddScore(); _scoreManager.UpdateScore(); });
        _playerView.HealthPlayerView.OnGetHelthCommand.Subscribe(_ => { _healthManager.AddHealth(); });
        _playerView.HealthPlayerView.OnGetHelthCommand.Subscribe(_ => { AudioManager.Instance.PlayGetHealth(); });
        _playerView.OnGetDamageCommand.Subscribe(_ => { _healthManager.Damage(); });
        _playerView.OnGetDamageCommand.Subscribe(_ => { AudioManager.Instance.PlayGrunt(); });
        _playerView.OnGameOverCommand.Subscribe(_ => { _gameOverView.SetActive(true); });
        _playerView.OnGameOverCommand.Subscribe(_ => { _scoreManager.OnGameOver(); });
        _playerView.OnGameOverCommand.Subscribe(_ => { AudioManager.Instance.PlayGameOver(); });
        _playerView.OnGetSunCommand.Subscribe(_ => { _levelManager.TimerRunningView.StarTimer(); });
        _playerView.OnGetSunCommand.Subscribe(_ => { _playerView.OnGetSun(); });
        _playerView.OnGetSunCommand.Subscribe(_ => { _player.SetRunningState(true); });
        _playerView.OnGetSunCommand.Subscribe(_ => { AudioManager.Instance.PlayGetSun(); });
        _playerView.WeaponView.OnFireCommand.Subscribe(_ => { AudioManager.Instance.PlayFire(); });

        _healthManager.GameOverCommand.Subscribe(_ => { _gameOverView.SetActive(true); });
        _healthManager.GameOverCommand.Subscribe(_ => { _scoreManager.OnGameOver(); });
        _healthManager.GameOverCommand.Subscribe(_ => { AudioManager.Instance.PlayGameOver(); });

        _levelManager.TimerRunningView.OnEndTimerCommand.Subscribe(_ => { _playerController.OnEndTimerRunning(); });

        _player.Speed.Subscribe(_ => { _levelManager.CheckingInvisibleFrameMaps(_playerView.transform.position); });
        AddObjectsDisposable();
    }

    private void FixedUpdate()
    {
        _playerController.FixedUpdate();
    }

    private void AddObjectsDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_playerView.HealthPlayerView.OnGetHelthCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetBulletCommand);
        ManagerUniRx.AddObjectDisposable(_player.Speed);
        ManagerUniRx.AddObjectDisposable(_playerView.OnCollisionGroundCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetDamageCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetCrystalCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGameOverCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetSunCommand);
        ManagerUniRx.AddObjectDisposable(_levelManager.TimerRunningView.OnEndTimerCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.WeaponView.OnFireCommand);
        _levelManager.AddObjectsDisposable();
    }
}
