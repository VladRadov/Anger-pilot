using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player
{
    private bool _isStandGround;
    private bool _isRunning;

    public Player()
    {
        _isStandGround = false;
        _isRunning = false;
    }

    public ReactiveProperty<Vector2> Speed = new();
    public bool IsRunning => _isRunning;
    public bool IsStandGround => _isStandGround;

    public void Jump(Vector2 speed)
    {
        if (_isStandGround)
        {
            Speed.Value = speed;
            _isStandGround = false;
        }
    }

    public void SetRunningState(bool value)
        => _isRunning = value;

    public void Running(Vector2 speed)
        => Speed.Value = speed;

    public void OnCollisionGround()
    {
        _isStandGround = true;
        Speed.Value = Vector2.zero;
    }
}
