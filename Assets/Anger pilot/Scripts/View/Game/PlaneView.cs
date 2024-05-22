using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneView : MonoBehaviour
{
    private bool _isCrash;

    [SerializeField] private Animator _animator;

    public ReactiveCommand OnCrashPlaneCommand = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void PlayAnimationCrash()
    {
        AudioManager.Instance.PlayCrashPlane();
        _animator.SetTrigger("IsCrash");
    }

    private void Start()
    {
        _isCrash = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCrash == false && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayAnimationCrash();
            OnCrashPlaneCommand.Execute();
            _isCrash = true;
        }
    }

    private void OnValidate()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnCrashPlaneCommand);
    }
}
