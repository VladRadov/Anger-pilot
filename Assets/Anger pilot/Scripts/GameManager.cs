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
        _levelManager.Initialize(_playerView.transform);
        _healthManager.Initialize();
        _bulletManager.Initialize();

        _levelManager.SetPlayerSkin(_healthManager.CurrentSkin);

        _player = new Player();
        _playerController = new PlayerController(_playerView, _player);
        _playerController.Initialize();
        _playerView.Speed = Vector2.up * _playerView.ForceJumpUp.Value + Vector2.right * _playerView.ForceJumpLeft.Value;
        _playerView.SetSpriteHead(_healthManager.CurrentSkin.Icon);

        _fire.onClick.AddListener(() => { if (_bulletManager.TryFire()) _playerView.WeaponView.Fire(); });    

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
        _playerView.OnGameOverCommand.Subscribe(_ => { OnGameOver(); });
        _playerView.OnCollisionGroundCommand.Subscribe(_ => { OnCollisionGround(); });
        _playerView.OnGetSunCommand.Subscribe(_ => { OnGetSun(); });
        _playerView.WeaponView.OnFireCommand.Subscribe(_ => { OnFire(); });

        _healthManager.GameOverCommand.Subscribe(_ => { _gameOverView.SetActive(true); });
        _healthManager.GameOverCommand.Subscribe(_ => { _scoreManager.SaveCrystalsOfGame(); });
        _healthManager.GameOverCommand.Subscribe(_ => { AudioManager.Instance.PlayGameOver(); });
        _healthManager.GameOverCommand.Subscribe(_ => { _levelManager.VibrationIphone(); });

        _levelManager.TimerRunningView.OnEndTimerCommand.Subscribe(_ => { OnTimerRunnigEnd(); });
        _levelManager.PauseView.OnActiverPauseCommand.Subscribe(value => { OnSetActivePause(value); });
        _levelManager.OnJumpInTreeCommand.Subscribe(value => { OnJumpInTree(value); });
        _levelManager.SubscribeOnMouseUp(_ => { OnMouseUpInputSystem(); });
        _levelManager.SubscribeWolfsOnGameOver(_playerView.OnGameOverCommand, _playerView.transform);
        _levelManager.SubscribeOnMouseDown(_ => { OnMouseDownInputSystem(); });
        _levelManager.PlaneView.OnCrashPlaneCommand.Subscribe(_ => { OnCrashPlane(); });

        _player.Speed.Subscribe(_ => { _levelManager.CheckingInvisibleFrameMaps(_playerView.transform.position); });
        AddObjectsDisposable();
    }

    private void OnCrashPlane()
    {
        _levelManager.SearchNearTree(_playerView.transform.position);
        _playerView.SetActive(true);
        _playerController.OnEndTimerRunning();
    }

    private async void OnFire()
    {
        AudioManager.Instance.PlayFire();
        _playerView.AnimationController.PlayFire();

        await Task.Delay(1000);

        if (_player.IsRunning)
            _playerView.AnimationController.PlayRun();
    }

    private void OnMouseUpInputSystem()
    {
        _playerController.SetMouseHolding(false);

        if (_levelManager.IsGameOver)
            return;
        if (_player.IsRunning == false)
            _playerView.AnimationController.PlayRunOne();
    }

    private void OnMouseDownInputSystem()
    {
        if (_levelManager.IsGameOver)
            return;

        if (_player.IsStandPlaceJump)
        {
            _playerView.AnimationController.PlayRun();
            _playerController.SetMouseHolding(true);
        }

        if (_player.IsRunning)
            _playerController.Jump(Vector2.up * _playerView.ForceJumpUp.Value);
    }

    private void OnJumpInTree(Vector3 value)
    {
        _playerView.SetBodyType(RigidbodyType2D.Static);
        _playerController.SetPositionTree(value);
    }

    private void OnCollisionGround()
    {
        if (_playerController.JumpingForRunning != Vector2.zero)
        {
            _playerController.SetForceJumpingForRunning(Vector2.zero);
            if (_player.IsRunning)
                _playerView.AnimationController.PlayRun();
        }
    }

    private void OnSetActivePause(bool value)
    {
        if (value)
        {
            _levelManager.TimerRunningView.StopTimer();
            _playerView.SetBodyType(RigidbodyType2D.Static);
            _playerView.AnimationController.Pause();
        }
        else
        {
            _levelManager.TimerRunningView.ContinueTimer();
            _playerView.SetBodyType(RigidbodyType2D.Dynamic);
            _playerView.AnimationController.Continue();
        }
        _levelManager.SetPause(value);
    }

    private async void OnGameOver()
    {
        if (_levelManager.IsGameOver == false)
        {
            _levelManager.GameOver();
            AudioManager.Instance.PlayGameOver();
            _fire.onClick.RemoveAllListeners();
            _levelManager.VibrationIphone();
            await Task.Delay(3000);
            if(_gameOverView != null)
                _gameOverView.SetActive(true);
            _scoreManager.SaveCrystalsOfGame();
        }
    }

    private void OnTimerRunnigEnd()
    {
        _levelManager.SetBGFrameMaps();
        _levelManager.SearchNearTree(_playerView.transform.position);
        _playerController.OnEndTimerRunning();
        _levelManager.FrameMapController.SetActiveEnemyes(true);
    }

    private async void OnGetSun()
    {
        if (_player.IsRunning)
            return;

        _playerView.OnGetSun();
        _playerController.SetForceJumpingForRunning(Vector2.down * _playerView.ForceJumpUp.Value * 2);
        _levelManager.SetBGFramMapsDay();
        AudioManager.Instance.PlayGetSun();
        _levelManager.VibrationIphone();
        _levelManager.FrameMapController.SetActiveEnemyes(false);
        await Task.Delay(200);
        _levelManager.TimerRunningView.StarTimer();
        _player.SetRunningState(true);
        _playerView.AnimationController.PlayRun();
    }

    private void OnCollisionPlaceJump()
    {
        if (_player.IsStandPlaceJump == false)
        {
            _player.OnCollisionPlaceJump();
            _scoreManager.AddScore();
            _scoreManager.UpdateScore();
            _levelManager.VibrationIphone();
        }
    }

    private void FixedUpdate()
    {
        if (_levelManager.IsPause == false)
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
        ManagerUniRx.AddObjectDisposable(_levelManager.PauseView.OnActiverPauseCommand);
        ManagerUniRx.AddObjectDisposable(_levelManager.OnJumpInTreeCommand);
        ManagerUniRx.AddObjectDisposable(_levelManager.PlaneView.OnCrashPlaneCommand);
        _levelManager.AddObjectsDisposable();
    }
}
