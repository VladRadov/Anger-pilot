using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayJump()
    {
        _animator.Play("PlayerJump", -1, 0);
    }

    public void PlayRunOne()
    {
        _animator.Play("PlayerRunOne", -1, 0);
    }

    public void PlayFire()
    {
        _animator.Play("Fire", -1, 0);
    }

    public void PlayRun()
    {
        _animator.Play("PlayerRun", -1, 0);
    }

    public void Pause()
        => _animator.speed = 0;

    public void Continue()
        => _animator.speed = 1;

    private void OnValidate()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}
