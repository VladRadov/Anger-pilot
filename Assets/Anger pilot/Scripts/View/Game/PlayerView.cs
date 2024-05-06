using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 _test;

    private Vector2 _currentSpeed;

    public FloatReactiveProperty ForceJumpUp = new();
    public FloatReactiveProperty ForceJumpLeft = new();

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            _currentSpeed = Vector2.up * ForceJumpUp.Value + Vector2.right * ForceJumpLeft.Value;

        if (_currentSpeed != Vector2.zero)
        {
            _rigidbody2D.velocity = _currentSpeed;
            _currentSpeed -= _test;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentSpeed = Vector2.zero;
    }

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
