using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private PolygonCollider2D _polygonCollider;

    public void SetSkin(Sprite icon)
        => _skin.sprite = icon;

    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

    private void OnValidate()
    {
        if (_polygonCollider == null)
            _polygonCollider = GetComponent<PolygonCollider2D>();

        if (_skin == null)
            _skin = GetComponent<SpriteRenderer>();
    }
}
