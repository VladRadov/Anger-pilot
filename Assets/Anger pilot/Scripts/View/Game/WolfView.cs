using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfView : MonoBehaviour
{
    private Vector2 _positionPlayer;
    private Vector3 _positionPatrul;
    private Transform _transform;
    private bool _isStopped;
    private bool _isRunning;
    private bool _isVisible;

    [SerializeField] private Animator _animator;
    [SerializeField] private Vector3 _offsetPositionPatrul;

    public void OnPlayerCollisionGround(Transform positionPlayer)
    {
        if (_isRunning || _isStopped)
            return;

        if (_isVisible)
        {
            _isRunning = true;

            if (_offsetPositionPatrul.x < 0 && positionPlayer.position.x > _transform.position.x || _offsetPositionPatrul.x > 0 && positionPlayer.position.x < _transform.position.x)
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
    {
        _animator.speed = 1;
        _animator.Play("RunningWolf", -1, 0);
    }

    private void StopAnimatioRunning()
    {
        _animator.speed = 0;
        _animator.Play("RunningWolf", -1, 0);
    }

    private void Start()
    {
        _positionPlayer = Vector2.zero;
        _transform = transform;
        _isStopped = false;
        _isRunning = false;
    }

    private void FixedUpdate()
    {
        if (_positionPlayer != Vector2.zero && _isStopped == false)
            _transform.position = Vector2.MoveTowards(_transform.position, _positionPlayer, 0.3f);
        else if (_isStopped == false && _positionPlayer == Vector2.zero && _isVisible)
        {
            if (Mathf.Abs(Vector3.Distance(_transform.position, _positionPatrul)) > 0.2)
                _transform.position = Vector2.MoveTowards(_transform.position, _positionPatrul, 0.3f);
            else
            {
                _offsetPositionPatrul *= -1;
                _positionPatrul = _transform.position + _offsetPositionPatrul;
                Rotation();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerView>();
        if (player != null && _isStopped == false)
        {
            _isStopped = true;
            StopAnimatioRunning();
        }
    }

    private void OnBecameVisible()
    {
        _isVisible = true;
        AudioManager.Instance.PlayWolf();
        PlayAdimatioRunning();
        _positionPatrul = _transform.position + _offsetPositionPatrul;
    }

    private void OnBecameInvisible()
    {
        _isVisible = false;
    }
}
