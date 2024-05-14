using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player
{
    private bool _isStandPlaceJump;
    private bool _isRunning;
    private bool _isJumpOnTree;

    public Player()
    {
        _isStandPlaceJump = false;
        _isRunning = false;
        _isJumpOnTree = false;
    }

    public ReactiveProperty<Vector2> Speed = new();
    public bool IsRunning => _isRunning;
    public bool IsStandPlaceJump => _isStandPlaceJump;
    public bool IsJumpOnTree => _isJumpOnTree;

    public void Jump(Vector2 speed)
    {
        if (_isStandPlaceJump)
        {
            Speed.Value = speed;
            _isStandPlaceJump = false;
        }
    }

    public void SetJumpOnTreeState(bool value)
        => _isJumpOnTree = value;

    public void SetRunningState(bool value)
        => _isRunning = value;

    public void Running(Vector2 speed)
        => Speed.Value = speed;

    public void OnCollisionPlaceJump()
    {
        _isStandPlaceJump = true;
        Speed.Value = Vector2.zero;
    }
}
