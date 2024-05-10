using System.Threading.Tasks;
using UnityEngine;
using UniRx;

public class PlayerController
{
    private PlayerView _view;
    private Player _player;
    private bool _isMouseHolding;

    public PlayerController(PlayerView view, Player player)
    {
        _view = view;
        _player = player;
    }

    public void Initialize()
    {
        _player.Speed.Subscribe(value => { _view.UpdateSpeed(value); });
    }

    public void SetMouseHolding(bool value)
        => _isMouseHolding = value;

    public void FixedUpdate()
    {
        if (_player.IsRunning)
        {
            _player.Running(Vector2.right * _view.ForceJumpLeft.Value);
            _view.UpdateSpeed(Vector2.right * _view.ForceJumpLeft.Value);
        }

        if (_isMouseHolding)
        {
            _player.Jump(_view.Speed);
            _view.UpdateSpeed(_view.Speed);
        }
    }

    public async void OnEndTimerRunning()
    {
        _player.SetRunningState(false);
        _player.OnCollisionGround();
        _player.Jump(Vector2.up * _view.ForceJumpUp.Value * 3.5f + Vector2.right * _view.ForceJumpLeft.Value);
        await Task.Delay(500);
        _view.OnEndTimerRunnning();
        _player.OnCollisionGround();
    }

    public void Dispose()
    {
        ManagerUniRx.Dispose(_player.Speed);
    }
}
