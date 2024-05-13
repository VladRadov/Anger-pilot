using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfView : MonoBehaviour
{
    private bool _isVisible;
    private Vector2 _positionPlayer;
    private Transform _transform;

    [SerializeField] private Animator _animator;

    public void OnPlayerCollisionGround(Transform positionPlayer)
    {
        if (_isVisible)
        {
            if (positionPlayer.position.x > _transform.position.x)
                Rotation();

            _positionPlayer = new Vector2(positionPlayer.position.x, _transform.position.y);
            PlayAdimatioRunning();
        }
    }

    public void Rotation()
    {
        if (_transform == null)
            _transform = transform;

        _transform.Rotate(0, 180, 0);
    }

    private void PlayAdimatioRunning()
        => _animator.speed = 1;

    private void StopAnimatioRunning()
    {
        _animator.speed = 0;
        _animator.Play("RunningWolf", -1, 0);
    }

    private void Start()
    {
        _positionPlayer = Vector2.zero;
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_positionPlayer != Vector2.zero && Vector2.Distance(_transform.position, _positionPlayer) > 0.3)
            _transform.position = Vector2.MoveTowards(_transform.position, _positionPlayer, 0.3f);
        else
            StopAnimatioRunning();
    }

    private void OnBecameVisible()
    {
        _isVisible = true;
        AudioManager.Instance.PlayWolf();
    }

    private void OnBecameInvisible()
    {
        _isVisible = false;
    }
}
