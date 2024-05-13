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

    public void PlayFire()
    {
        _animator.Play("Fire", -1, 0);
    }

    private void OnValidate()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}
