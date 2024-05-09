using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player
{
    private bool _isStandGround;

    public Player()
    {
        _isStandGround = false;
    }

    public ReactiveProperty<Vector2> Speed = new();

    public void Jump(Vector2 speed)
    {
        if (_isStandGround)
        {
            Speed.Value = speed;
            _isStandGround = false;
        }
    }

    public void OnCollisionGround()
    {
        _isStandGround = true;
        Speed.Value = Vector2.zero;
    }
}
