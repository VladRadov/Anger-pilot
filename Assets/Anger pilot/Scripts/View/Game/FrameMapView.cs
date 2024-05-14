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
    [SerializeField] private List<WolfView> _wolfs;
    [SerializeField] private List<TreeView> _trees;

    public SystemInput SystemInput =>  _systemInput;
    public Vector3 LocalPosition => transform.localPosition;
    public BGView BGView => _bGView;
    public HealthView PrefabHealth => _prefabHealth;
    public List<WolfView> Wolfs => _wolfs;
    public List<TreeView> Trees => _trees;
    public float PositionXEnd => transform.position.x + _widthFrame;

    public void SetSpriteGround(Sprite sprite)
        => _ground.sprite = sprite;

    public void SetWidthFrame(int widthFrame)
        => _widthFrame = widthFrame;

    public void SetLocalPosition(Vector3 startPosition)
        => transform.localPosition = startPosition;

    public void SetActive(bool value)
        => transform.gameObject.SetActive(value);

    public void SetActiveWols(bool value)
    {
        foreach (var wolf in _wolfs)
            wolf.gameObject.SetActive(value);
    }

    public void RotationWolfs()
    {
        foreach (var wolf in _wolfs)
            wolf.Rotation();
    }
}
