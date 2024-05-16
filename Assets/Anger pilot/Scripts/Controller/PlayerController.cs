using System.Threading.Tasks;
using UnityEngine;
using UniRx;

public class PlayerController
{
    private PlayerView _view;
    private Player _player;
    private bool _isMouseHolding;
    private Vector2 _jumpingForRunning;
    private Vector3 _positionTree;

    public PlayerController(PlayerView view, Player player)
    {
        _view = view;
        _player = player;
    }

    public Vector2 JumpingForRunning => _jumpingForRunning;

    public void Initialize()
    {
        _player.Speed.Subscribe(value => { _view.UpdateSpeed(value); });
    }

    public void SetForceJumpingForRunning(Vector2 forceJumping)
        => _jumpingForRunning = forceJumping;

    public void SetPositionTree(Vector2 positionTree)
        => _positionTree = positionTree;

    public async void Jump(Vector2 forceJumping)
    {
        if (_jumpingForRunning != Vector2.zero)
            return;

        _view.AnimationController.PlayJump();
        _jumpingForRunning = forceJumping;
        await Task.Delay(500);
        if(_jumpingForRunning != Vector2.down * forceJumping)
            _jumpingForRunning = Vector2.down * forceJumping;
    }

    public void SetMouseHolding(bool value)
        => _isMouseHolding = value;

    public void FixedUpdate()
    {
        if (_player.IsJumpOnTree && Vector3.Distance(_view.transform.position, _positionTree) > 0.3)
            _view.UpdatePosition(Vector3.MoveTowards(_view.transform.position, _positionTree, 0.7f));
        else
        {
            _view.SetBodyType(RigidbodyType2D.Dynamic);
            _player.SetJumpOnTreeState(false);
        }

        if (_player.IsRunning)
        {
            _player.Running(_jumpingForRunning + Vector2.right * _view.ForceJumpLeft.Value);
            _view.UpdateSpeed(_jumpingForRunning + Vector2.right * _view.ForceJumpLeft.Value);
        }

        if (_isMouseHolding)
        {
            _player.Jump(_view.Speed);
            _view.UpdateSpeed(_view.Speed);
        }
    }

    public async void OnEndTimerRunning()
    {
        _view.AnimationController.PlayJump();
        _player.SetRunningState(false);
        _player.SetJumpOnTreeState(true);
        _player.OnCollisionPlaceJump();
        await Task.Delay(500);
        _view.OnEndTimerRunnning();
        _player.OnCollisionPlaceJump();
    }

    public void Dispose()
    {
        ManagerUniRx.Dispose(_player.Speed);
    }
}
