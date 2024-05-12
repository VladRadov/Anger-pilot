using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        _playerView.SetSpriteHead(_healthManager.CurrentSkin.Icon);

        _fire.onClick.AddListener(() => { if (_bulletManager.TryFire()) _playerView.WeaponView.Fire(); });

        _levelManager.SubscribeOnMouseDown(_ =>
        {
            if (_player.IsStandGround)
                _playerController.SetMouseHolding(true);

            if (_player.IsRunning)
                _playerController.SetForceJumpingForRunning(Vector2.up * _playerView.ForceJumpUp.Value);
        });
        _levelManager.SubscribeOnMouseUp(_ =>
        {
            _playerController.SetMouseHolding(false);
            if (_player.IsRunning)
                _playerController.SetForceJumpingForRunning(Vector2.down * _playerView.ForceJumpUp.Value);
        });

        _playerView.OnGetCrystalCommand.Subscribe(_ => { _scoreManager.AddCrystal(); _scoreManager.UpdateScrystal(); });
        _playerView.OnGetCrystalCommand.Subscribe(_ => { _levelManager.VibrationIphone(); });
        _playerView.OnGetCrystalCommand.Subscribe(_ => { AudioManager.Instance.PlayGetCrystal(); });
        _playerView.OnGetBulletCommand.Subscribe(_ => { _bulletManager.AddBullet(); });
        _playerView.OnCollisionPlaceJumpCommand.Subscribe(_ => { OnCollisionPlaceJump(); });
        _playerView.HealthPlayerView.OnGetHelthCommand.Subscribe(_ => { _healthManager.AddHealth(); });
        _playerView.HealthPlayerView.OnGetHelthCommand.Subscribe(_ => { AudioManager.Instance.PlayGetHealth(); });
        _playerView.HealthPlayerView.OnGetHelthCommand.Subscribe(_ => { _levelManager.VibrationIphone(); });
        _playerView.OnGetDamageCommand.Subscribe(_ => { _levelManager.VibrationIphone(); });
        _playerView.OnGetDamageCommand.Subscribe(_ => { _healthManager.Damage(); });
        _playerView.OnGetDamageCommand.Subscribe(_ => { AudioManager.Instance.PlayGrunt(); });
        _playerView.OnGameOverCommand.Subscribe(_ => { _gameOverView.SetActive(true); });
        _playerView.OnGameOverCommand.Subscribe(_ => { _scoreManager.OnGameOver(); });
        _playerView.OnGameOverCommand.Subscribe(_ => { AudioManager.Instance.PlayGameOver(); });
        _playerView.OnGameOverCommand.Subscribe(_ => { _levelManager.VibrationIphone(); });
        _playerView.OnCollisionGroundCommand.Subscribe(_ => { if(_playerController.JumpingForRunning != Vector2.zero) _playerController.SetForceJumpingForRunning(Vector2.zero); });

        _playerView.OnGetSunCommand.Subscribe(_ => { OnGetSun(); });

        _playerView.WeaponView.OnFireCommand.Subscribe(_ => { AudioManager.Instance.PlayFire(); });

        _healthManager.GameOverCommand.Subscribe(_ => { _gameOverView.SetActive(true); });
        _healthManager.GameOverCommand.Subscribe(_ => { _scoreManager.OnGameOver(); });
        _healthManager.GameOverCommand.Subscribe(_ => { AudioManager.Instance.PlayGameOver(); });
        _healthManager.GameOverCommand.Subscribe(_ => { _levelManager.VibrationIphone(); });

        _levelManager.TimerRunningView.OnEndTimerCommand.Subscribe(_ => { OnTimerRunnigEnd(); });

        _player.Speed.Subscribe(_ => { _levelManager.CheckingInvisibleFrameMaps(_playerView.transform.position); });
        AddObjectsDisposable();
    }

    private void OnTimerRunnigEnd()
    {
        _levelManager.SetBGFrameMaps();
        _playerController.OnEndTimerRunning();
        _levelManager.FrameMapController.SetActiveEnemyes(true);
    }

    private async void OnGetSun()
    {
        _levelManager.SetBGFramMapsDay();
        AudioManager.Instance.PlayGetSun();
        _levelManager.VibrationIphone();
        _playerView.OnGetSun();
        _levelManager.FrameMapController.SetActiveEnemyes(false);
        await Task.Delay(200);
        _levelManager.TimerRunningView.StarTimer();
        _player.SetRunningState(true);
    }

    private void OnCollisionPlaceJump()
    {
        if (_player.IsStandGround == false)
        {
            _player.OnCollisionGround();
            _scoreManager.AddScore();
            _scoreManager.UpdateScore();
            _levelManager.VibrationIphone();
        }
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
        ManagerUniRx.AddObjectDisposable(_playerView.OnCollisionPlaceJumpCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetDamageCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetCrystalCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGameOverCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnGetSunCommand);
        ManagerUniRx.AddObjectDisposable(_levelManager.TimerRunningView.OnEndTimerCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.WeaponView.OnFireCommand);
        ManagerUniRx.AddObjectDisposable(_playerView.OnCollisionGroundCommand);
        _levelManager.AddObjectsDisposable();
    }
}
