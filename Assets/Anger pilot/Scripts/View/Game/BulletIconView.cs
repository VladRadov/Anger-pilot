using UnityEngine;
using UnityEngine.UI;

public class BulletIconView : BulletView
{
    [SerializeField] private Image _icon;

    public override void SetActiveIcon(bool value)
        => _icon.sprite = value ? _iconActive : _iconNoActive;

    private void OnValidate()
    {
        if (_icon == null)
            _icon = GetComponent<Image>();
    }
}
