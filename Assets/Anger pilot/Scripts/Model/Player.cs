using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player
{
    private bool _isStandPlaceJump;
    private bool _isRunning;

    public Player()
    {
        _isStandPlaceJump = false;
        _isRunning = false;
    }

    public ReactiveProperty<Vector2> Speed = new();
    public bool IsRunning => _isRunning;
    public bool IsStandGround => _isStandPlaceJump;

    public void Jump(Vector2 speed)
    {
        if (_isStandPlaceJump)
        {
            Speed.Value = speed;
            _isStandPlaceJump = false;
        }
    }

    public void SetRunningState(bool value)
        => _isRunning = value;

    public void Running(Vector2 speed)
        => Speed.Value = speed;

    public void OnCollisionGround()
    {
        _isStandPlaceJump = true;
        Speed.Value = Vector2.zero;
    }
}
