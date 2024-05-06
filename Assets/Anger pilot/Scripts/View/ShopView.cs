using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private List<SkinItemShopView> _skins;
    [SerializeField] private List<GroundItemShopView> _grounds;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() => { SetActive(false); });

        foreach (var skin in _skins)
        {
            foreach (var skinTemp in _skins)
                skin.OnSelectedItemEventHander.AddListener(skinTemp.InitializeStateItem);
        }

        foreach (var ground in _grounds)
        {
            foreach (var groundTemp in _grounds)
                ground.OnSelectedItemEventHander.AddListener(groundTemp.InitializeStateItem);
        }
    }
}
