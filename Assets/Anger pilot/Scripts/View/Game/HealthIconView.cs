using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIconView : MonoBehaviour
{
    [SerializeField] private Image _viewIcon;
    [SerializeField] private Sprite _noHealth;

    public void SetIcon(Sprite icon)
        => _viewIcon.sprite = icon;

    public void SetIconNoHealth(Sprite icon)
        => _noHealth = icon;

    public void DestroyHealth()
        => SetIcon(_noHealth);

    private void OnValidate()
    {
        if (_viewIcon == null)
            _viewIcon = GetComponent<Image>();
    }
}
