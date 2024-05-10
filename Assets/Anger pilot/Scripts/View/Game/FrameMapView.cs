using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameMapView : MonoBehaviour
{
    private int _widthFrame;

    [SerializeField] private SystemInput _systemInput;
    [SerializeField] private BGView _bGView;
    [SerializeField] private HealthView _prefabHealth;
    [SerializeField] private SpriteRenderer _ground;

    public SystemInput SystemInput =>  _systemInput;
    public Vector3 LocalPosition => transform.localPosition;
    public BGView BGView => _bGView;
    public HealthView PrefabHealth => _prefabHealth;
    public float PositionXEnd => transform.position.x + _widthFrame;

    public void SetSpriteGround(Sprite sprite)
        => _ground.sprite = sprite;

    public void SetWidthFrame(int widthFrame)
        => _widthFrame = widthFrame;

    public void SetLocalPosition(Vector3 startPosition)
        => transform.localPosition = startPosition;

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);
}
