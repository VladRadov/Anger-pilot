using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BGView : MonoBehaviour
{
    private bool _isVisible;
    private Transform _transform;

    [SerializeField] private FrameMapView _frameMapView;

    public bool IsVisible => _isVisible;
    public ReactiveCommand<FrameMapView> OnInvisibleFrameMapCommand = new();

    public void CheckingFrameOnPassed(Vector3 positionPlayer)
    {
        if (_frameMapView.PositionXEnd < positionPlayer.x)
        {
            var parent = _transform.parent.gameObject.GetComponent<FrameMapView>();

            if(parent != null)
                OnInvisibleFrameMapCommand.Execute(parent);
        }
    }

    private void Awake()
    {
        _isVisible = true;
        _transform = transform;
    }

    private void OnBecameInvisible()
    {
        _isVisible = false;
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnInvisibleFrameMapCommand);
    }
}
