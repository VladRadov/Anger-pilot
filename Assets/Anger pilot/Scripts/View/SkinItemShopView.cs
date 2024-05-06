using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinItemShopView : ItemShopView
{
    [SerializeField] private string _nameSkin;

    public override void InitializeStateItem()
    {
        var isCurrentSkin = ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin == _nameSkin;
        var isPurchased = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins.Contains(_nameSkin);

        if (isCurrentSkin)
            SetSelectedState();
        else if (isPurchased)
            SetPurchasedState();
        else
            SetPurchaseState();
    }

    protected override void Purchase()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins += _nameSkin + ";";
    }

    protected override void OnSelectedItem()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin = _nameSkin;
        base.OnSelectedItem();
    }
}
