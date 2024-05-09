using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HealthManager : MonoBehaviour
{
    private int _currentHealth;
    private List<HealthIconView> _healthIcons;
    private SkinItem _currentSkin;

    [SerializeField] private List<SkinItem> _skins;
    [SerializeField] private int _countHealth;
    [SerializeField] private HealthIconView _prefabHhealthIconView;
    [SerializeField] private GameObject _healthLine;

    public ReactiveCommand GameOverCommand = new();
    public SkinItem CurrentSkin => _currentSkin;

    public void Damage()
    {
        if (_currentHealth != 0)
        {
            _currentHealth -= 1;
            _healthIcons[_currentHealth].DestroyHealth();
        }
        else
            GameOverCommand.Execute();
    }

    public void AddHealth()
    {
        if (_currentHealth != _countHealth)
        {
            _healthIcons[_currentHealth].SetIcon(_currentSkin.Icon);
            _currentHealth += 1;
        }
    }

    public void Initialize()
    {
        _healthIcons = new List<HealthIconView>();
        _currentHealth = _countHealth;
        _currentSkin = _skins.Find(skin => skin.Name == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin);

        for (int i = 0; i < _currentHealth; i++)
        {
            var healthIcon = Instantiate(_prefabHhealthIconView, _healthLine.transform);
            healthIcon.SetIcon(_currentSkin.Icon);
            healthIcon.SetIconNoHealth(_currentSkin.SkinNoActive);
            _healthIcons.Add(healthIcon);
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(GameOverCommand);
    }
}
