using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private List<SkinItemShopView> _skins;
    [SerializeField] private List<GroundItemShopView> _grounds;
    [SerializeField] private Text _viewCrystal;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        UpdateCrystal();

        _close.onClick.AddListener(() => { SetActive(false); });
        _close.onClick.AddListener(() => { AudioManager.Instance.PlayClickButton(); });

        foreach (var skin in _skins)
        {
            skin.OnPurchasedItemEventHander.AddListener(UpdateCrystal);
            foreach (var skinTemp in _skins)
                skin.OnSelectedItemEventHander.AddListener(skinTemp.InitializeStateItem);
        }

        foreach (var ground in _grounds)
        {
            ground.OnPurchasedItemEventHander.AddListener(UpdateCrystal);
            foreach (var groundTemp in _grounds)
                ground.OnSelectedItemEventHander.AddListener(groundTemp.InitializeStateItem);
        }
    }

    private void UpdateCrystal()
        => _viewCrystal.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.AllCrystal.ToString();
}
